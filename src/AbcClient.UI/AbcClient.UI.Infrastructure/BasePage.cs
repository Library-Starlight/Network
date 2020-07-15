using AbcClient.Core.DI;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace AbcClient.UI.Infrastructure
{
    /// <summary>
    /// The base page for all pages to gain base functionality
    /// </summary>
    public class BasePage : UserControl
    {
        #region Private Member

        /// <summary>
        /// The View Model associated with this page
        /// </summary>
        private object mViewModel;

        #endregion

        #region Public Properties

        /// <summary>
        /// The animation to play when the page is first loaded
        /// </summary>
        public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.SlideAndFadeInFromRight;

        /// <summary>
        /// The animation to play when the page is unloaded
        /// </summary>
        public PageAnimation PageUnloadAnimation { get; set; } = PageAnimation.SlideAndFadeOutToLeft;

        /// <summary>
        /// The time any slide animation takes to complete
        /// </summary>
        public float SlideSeconds { get; set; } = 0.4F;

        /// <summary>
        /// A flag to indicate if this page should animate out on load
        /// Useful for when we are moving the page to another frame
        /// </summary>
        public bool ShouldAnimateOut { get; set; }

        /// <summary>
        /// The View Model associated with this page
        /// </summary>
        public object ViewModelObject
        {
            get => mViewModel;
            set
            {
                // If nothing has changed, return
                //This is not able to compiled if there is not a BaseViewModel constraint.
                if (mViewModel == value)
                    return;

                // Update the value
                mViewModel = value;

                // Fire the view model changed method
                OnViewModelChanged();

                // Set the data context for the page
                DataContext = mViewModel;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public BasePage()
        {
            // Don't bother animating in design time
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            // If we are animating in, hide to begin with
            if (this.PageLoadAnimation != PageAnimation.None)
                this.Visibility = Visibility.Collapsed;

            // Listen out for the page loading
            this.Loaded += BasePage_LoadedAsync;
        }

        #endregion

        #region Animation Load / Unload

        /// <summary>
        /// Once the page is loaded, perform any required animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BasePage_LoadedAsync(object sender, System.Windows.RoutedEventArgs e)
        {
            // If we are setup to animate out on load
            if (ShouldAnimateOut)
                await AnimateOutAsync();
            else
                // Animate the page in
                await AnimateInAsync();
        }

        /// <summary>
        /// Animates the page in
        /// </summary>
        /// <returns></returns>
        public async Task AnimateInAsync()
        {
            if (this.PageLoadAnimation == PageAnimation.None)
                return;

            switch (PageLoadAnimation)
            {
                case PageAnimation.SlideAndFadeInFromRight:

                    // Start the animation
                    await this.SlideAndFadeInAsync(AnimationSlideInDirection.Right, false, this.SlideSeconds, size: (int)Application.Current.MainWindow.Width);
                    break;
            }
        }

        /// <summary>
        /// Animates the page out
        /// </summary>
        /// <returns></returns>
        public async Task AnimateOutAsync()
        {
            if (this.PageUnloadAnimation == PageAnimation.None)
                return;

            switch (PageUnloadAnimation)
            {
                case PageAnimation.SlideAndFadeOutToLeft:

                    // Start the animation
                    await this.SlideAndFadeOutAsync(AnimationSlideInDirection.Left, this.SlideSeconds);
                    break;
            }
        }

        #endregion

        /// <summary>
        /// Fired when the view model changes
        /// </summary>
        protected virtual void OnViewModelChanged()
        {

        }
    }

    /// <summary>
    /// A base page for all pages to gain base functionality
    /// </summary>
    /// <typeparam name="VM">The View Model</typeparam>
    public class BasePage<VM> : BasePage
        where VM : BaseViewModel, new()
    {
        #region Public Properties

        public VM ViewModel
        {
            get => (VM)ViewModelObject;
            set => ViewModelObject = value;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor for xaml page
        /// </summary>
        /// <param name="viewModel">The specific view model to use, if any</param>
        public BasePage() : base()
        {
            // If in design time mode...
            if (DesignerProperties.GetIsInDesignMode(this))
                // Just use a new instance of the VM
                ViewModel = new VM();
            else
                // Create a default view model
                ViewModel = CoreDI.GetService<VM>() ?? new VM();
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="viewModel">The specific view model to use, if any</param>
        public BasePage(VM specificViewModel = null) : base()
        {
            // Set specific view model
            if (specificViewModel != null)
                ViewModel = specificViewModel;
            else
            {
                // If in design time mode...
                if (DesignerProperties.GetIsInDesignMode(this))
                    // Just use a new instance of the VM
                    ViewModel = new VM();
                else
                    // Create a default view model
                    ViewModel = CoreDI.GetService<VM>() ?? new VM();
            }
        }

        #endregion

    }
}
