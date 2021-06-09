namespace Training.ViewModel
{
    using MVVMCore;

    class MainViewModel : INotify
    {
        private object currentView;

        public object CurrentView
        {
            get => currentView;
            set
            {
                currentView = value;
                OnPropertyChanged();
            }
        }

        public ImageCheckViewModel ImageCheckVM { get; set; }
        public AutoCompleteViewModel AutoCompleteVM { get; set; }


        public BaseCommand ImageCheckCommand { get; set; }
        public BaseCommand AutoCompleteCommand { get; set; }

        public MainViewModel()
        {
            ImageCheckVM = new ImageCheckViewModel();
            AutoCompleteVM = new AutoCompleteViewModel();

            CurrentView = ImageCheckVM;

            ImageCheckCommand = new BaseCommand(o =>
            {
                CurrentView = ImageCheckVM;
            });
            AutoCompleteCommand = new BaseCommand(o =>
            {
                CurrentView = AutoCompleteVM;
            });
        }
    }
}
