using System;

public class EndScreen : Window
{
    public event Action EndButtonClicked;

    public override void Open()
    {
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    protected override void OnButtonClick()
    {
        EndButtonClicked?.Invoke();
    }
}
