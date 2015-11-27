using System;
using Java.Lang;

namespace Clock
{
	public class ClockAdapter : Android.Support.V4.App.FragmentPagerAdapter
	{
        Android.Support.V4.App.Fragment[] _fragments;
        ICharSequence[] _titles;


		public ClockAdapter(Android.Support.V4.App.FragmentManager fm, Android.Support.V4.App.Fragment[] fragments, ICharSequence[] titles)
			: base(fm)
		{
            _fragments = fragments;
            _titles = titles;
		}

		public override int Count
		{
			get
			{
                return _fragments.Length;
			}
		}

		public override Android.Support.V4.App.Fragment GetItem(int position)
		{
            return _fragments [position];
		}

		public override ICharSequence GetPageTitleFormatted(int position)
		{
            return _titles [position];
		}
	}
}