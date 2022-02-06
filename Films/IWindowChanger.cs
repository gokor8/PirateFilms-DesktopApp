namespace Films
{
    public interface IWindowChanger
    {
        public void CloseAndOpen();
        public void SetTransferAttribute<T>(T attribute);
    }
}