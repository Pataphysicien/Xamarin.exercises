using System;
using Android.Widget;
using System.Collections.Generic;
using Android.Views;
using System.IO;
using Android.Graphics.Drawables;

namespace XamarinUniversity
{
	public class InstructorAdapter : BaseAdapter<Instructor>, ISectionIndexer
	{
		List<Instructor> instructors;

		Java.Lang.Object[] sectionHeaders = SectionIndexerBuilder.BuildSectionHeaders (InstructorData.Instructors);
		Dictionary<int,int> positionForSectionMap = SectionIndexerBuilder.BuildPositionForSectionMap(InstructorData.Instructors);
		Dictionary<int,int> sectionForPositionMap = SectionIndexerBuilder.BuildSectionForPositionMap(InstructorData.Instructors);

		public InstructorAdapter (List<Instructor> instructors)
		{
			this.instructors = instructors;
		}

		#region implemented abstract members of BaseAdapter

		public override long GetItemId (int position)
		{
			return position;
		}

		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			var view = convertView;
			if (view == null)
			{
				var inflater = LayoutInflater.From (parent.Context);
				view = inflater.Inflate (Resource.Layout.InstructorRow, parent, false);
				var vh = new ViewHolder ();

				vh.Photo = view.FindViewById<ImageView> (Resource.Id.photoImageView);
				vh.Name = view.FindViewById<TextView> (Resource.Id.nameTextView);
				vh.Specialty = view.FindViewById<TextView> (Resource.Id.specialtyTextView);

				view.Tag = vh;
			}

			var viewHolder = (ViewHolder)view.Tag;

			var instructor = instructors [position];

//			Stream stream = parent.Context.Assets.Open (instructor.ImageUrl);
//			Drawable drawable = Drawable.CreateFromStream (stream, null);
			Drawable drawable = ImageAssetManager.Get(parent.Context, instructor.ImageUrl);
			viewHolder.Photo.SetImageDrawable (drawable);

			viewHolder.Name.Text = instructor.Name;
			viewHolder.Specialty.Text = instructor.Specialty;

			return view;
		}

		public override int Count {
			get {
				return this.instructors.Count;
			}
		}

		public override Instructor this [int index] {
			get {
				return this.instructors [index];
			}
		}

		#endregion

		public int GetPositionForSection (int sectionIndex)
		{
			return positionForSectionMap [sectionIndex];
		}

		public int GetSectionForPosition (int position)
		{
			return sectionForPositionMap [position];
		}

		public Java.Lang.Object[] GetSections ()
		{
			return sectionHeaders;
		}
	}
}

