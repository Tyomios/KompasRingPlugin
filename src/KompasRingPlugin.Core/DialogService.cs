using System.Windows.Threading;
using SecondaryWindow;
using SecondaryWindow.viewModels;

namespace Core;

public class DialogService
{
    private AdvancedDialogWindow _view;

    public static Dispatcher Dispatcher { get; set; }

    public DialogService()
    {
        Dispatcher.Invoke(() =>
        {
            _view = new();
        });
    }

    private BaseInfoVM _currentVM;

    private BaseInfoVM CurrentVM
    {
        get => _currentVM;
        set
        {
            _currentVM = value;
            if (_view is not null)
            {
                Dispatcher.Invoke(() =>
                {
                    _view.DataContext = _currentVM;
                });
            }
        }

    }

    public void ShowSuccessView(string? message, int delay)
    {
        Dispatcher.Invoke(() =>
        {
            CurrentVM = new SuccessVM(message is not null ? message : default);
            DialogEnd(delay);
        });
    }

    public void ShowWarningView(string message)
    {
        Dispatcher.Invoke(() =>
        {
            CurrentVM = new WarningVM(message);
        });
        
    }

    public void SetProgressData(string message, uint progress)
    {

        Dispatcher.Invoke(() =>
        {
            CurrentVM.Message = message;
            CurrentVM.Progress = progress;
        }); 
    }

    public void Start(BaseInfoVM viewModel)
    {
        CurrentVM = viewModel;
        _view.Show();
    }


    public void DialogEnd(int endDelay = 100)
    {
        System.Threading.Thread.Sleep(endDelay);
        _view.Close();
    }
}