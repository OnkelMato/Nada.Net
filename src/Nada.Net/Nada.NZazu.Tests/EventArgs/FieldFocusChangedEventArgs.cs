namespace Nada.NZazu.Tests.EventArgs;

public class FieldFocusChangedEventArgs : System.EventArgs
{
    public FieldFocusChangedEventArgs(INZazuWpfField newFocusedElement, INZazuWpfField oldFocusedElement = null,
        INZazuWpfField parentElement = null)
    {
        //NewFocusedElement = newFocusedElement ?? throw new ArgumentNullException(nameof(newFocusedElement));
        NewFocusedElement = newFocusedElement;
        OldFocusedElement = oldFocusedElement;
        ParentElement = parentElement;
    }

    public INZazuWpfField OldFocusedElement { get; }
    public INZazuWpfField NewFocusedElement { get; }
    public INZazuWpfField ParentElement { get; }
}