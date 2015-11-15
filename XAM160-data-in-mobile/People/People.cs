using System;

using Xamarin.Forms;

namespace People
{
    public class App : Application
    {
        public static PersonRepository PersonRepo { get; private set; }

        public App (string dbPath)
        {
            PersonRepo = new PersonRepository (dbPath);

            // The root page of your application
            this.MainPage = new People.MainPage();
        }

        protected override void OnStart ()
        {
            // Handle when your app starts
        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume ()
        {
            // Handle when your app resumes
        }
    }
}

