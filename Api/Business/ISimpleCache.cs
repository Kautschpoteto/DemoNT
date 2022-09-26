namespace Api.Business
{
    public interface ISimpleCache<T>
    {
        #region Public Methods

        T Get(string city);

        void Set(string key, T data);

        #endregion Public Methods
    }
}
