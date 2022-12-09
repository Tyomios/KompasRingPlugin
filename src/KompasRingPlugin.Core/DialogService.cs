using System.Windows.Threading;
using SecondaryWindow;

namespace Core;

public class DialogService
{
    private AdvancedDialogWindow _view;

    public DialogService(BaseInfoVM viewModel)
    {
        _view = new();
        CurrentVM = viewModel;
        _view.Show();
    }

    private BaseInfoVM _currentVM;

    public BaseInfoVM CurrentVM
    {
        get => _currentVM;
        set
        {
            _currentVM = value;
            if (_view is not null)
            {
                _view.DataContext = _currentVM;
            }
        }
        
    }

    public static Dispatcher Dispatcher { get; set; }

    public void DialogEnd(int endDelay = 100)
    {
        System.Threading.Thread.Sleep(endDelay);

        _view.Close();
    }
}