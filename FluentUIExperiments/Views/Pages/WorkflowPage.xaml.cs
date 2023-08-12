using Wpf.Ui.Common.Interfaces;

namespace FluentUIExperiments.Views.Pages;

public partial class WorkflowPage : INavigableView<ViewModels.WorkflowViewModel>
{
    public ViewModels.WorkflowViewModel ViewModel
    {
        get;
    }

    public WorkflowPage(ViewModels.WorkflowViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();
    }
}