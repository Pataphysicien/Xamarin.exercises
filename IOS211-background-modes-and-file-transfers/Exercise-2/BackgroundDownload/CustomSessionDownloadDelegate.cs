﻿using System;
using System.IO;
using Foundation;
using UIKit;

namespace BackgroundDownload
{
	/// <summary>
	/// Delegate used to control file transfers.
	/// </summary>
	public class CustomSessionDownloadDelegate : NSUrlSessionDownloadDelegate
	{
		public CustomSessionDownloadDelegate (DownloadController controller) : base()
		{
			this.controller = controller;
		}

		DownloadController controller;

		/// <summary>
		/// Gets called as we receive data.
		/// </summary>
		/// <param name="session">Session.</param>
		/// <param name="downloadTask">Download task.</param>
		/// <param name = "bytesWritten"></param>
		/// <param name="totalBytesWritten">Total bytes written.</param>
		/// <param name = "totalBytesExpectedToWrite"></param>
		public override void DidWriteData (NSUrlSession session, NSUrlSessionDownloadTask downloadTask, long bytesWritten, long totalBytesWritten, long totalBytesExpectedToWrite)
		{
			nuint localIdentifier = downloadTask.TaskIdentifier;
			float percentage = (float)totalBytesWritten / (float)totalBytesExpectedToWrite;
			Console.WriteLine ("DidWriteData - Task: {0}, BytesWritten: {1}, Total: {2}, Expected: {3}, Percentage: {4}", localIdentifier, bytesWritten, totalBytesWritten, totalBytesExpectedToWrite, percentage);

			// We are not on the UI thread here.
			this.InvokeOnMainThread (() => controller.UpdateProgress (percentage));
		}

		/// <summary>
		/// Gets called if the download has been completed.
		/// </summary>
		public override void DidFinishDownloading (NSUrlSession session, NSUrlSessionDownloadTask downloadTask, NSUrl location)
		{
			// The download location will be a file location.
			var sourceFile = location.Path;

			// Construct a destination file name.
			var destFile = downloadTask.OriginalRequest.Url.AbsoluteString.Substring(downloadTask.OriginalRequest.Url.AbsoluteString.LastIndexOf("/") + 1);

			Console.WriteLine ("DidFinishDownloading - Task: {0}, Source file: {1}", downloadTask.TaskIdentifier, sourceFile);

			// Copy over to documents folder. Note that we must use NSFileManager here! File.Copy() will not be able to access the source location.
			NSFileManager fileManager = NSFileManager.DefaultManager;

			// Create the filename
			var documentsFolderPath = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			NSUrl destinationURL = NSUrl.FromFilename(Path.Combine(documentsFolderPath, destFile));

			// Remove any existing file in our destination
			NSError error;
			fileManager.Remove(DownloadController.targetFilename, out error);
			bool success = fileManager.Copy(sourceFile, DownloadController.targetFilename, out error);
			if (!success)
			{
				Console.WriteLine ("Error during the copy: {0}", error.LocalizedDescription);
			}

			this.InvokeOnMainThread (() => this.controller.LoadImage());
		}

		/// <summary>
		/// Very misleading method name. Gets called if a download is done. Does not necessarily indicate an error
		/// unless the NSError parameter is not null.
		/// </summary>
		public override void DidCompleteWithError (NSUrlSession session, NSUrlSessionTask task, NSError error)
		{
			if (error == null)
			{
				return;
			}

			Console.WriteLine ("DidCompleteWithError - Task: {0}, Error: {1}", task.TaskIdentifier, error);

			task.Cancel ();
		}

		/// <summary>
		/// Gets called by iOS if all pending transfers are done. This will only be called if the app was backgrounded.
		/// </summary>
		public override void DidFinishEventsForBackgroundSession (NSUrlSession session)
		{
			// Nothing more to be done. This is the place where we have to call the completion handler we get passed in in AppDelegate.
			var handler = AppDelegate.BackgroundSessionCompletionHandler;
			AppDelegate.BackgroundSessionCompletionHandler = null;
			if (handler != null)
			{
				Console.WriteLine ("Calling completion handler.");
				this.controller.BeginInvokeOnMainThread(() => {
					new UIAlertView(string.Empty, "Selected files have been downloaded.", null, "OK").Show();

					// Bring up a local notification to take the user back to our app.
					Console.WriteLine ("Posting notification.");
					var notif = new UILocalNotification { 
						AlertBody = "Xamarin news: All pending files have been downloaded!"
					};
					UIApplication.SharedApplication.PresentLocalNotificationNow (notif);
					
					// Invoke the completion handler. This will tell iOS to update the snapshot in the task manager.
					handler.Invoke ();
				});
			}
		}
	}
}

