using System;

namespace EMX.JobyJobs.ProxyServices.Managers
{
    public class GenericEventArgs<TData> : EventArgs
    {
        public TData Data { get; set; }

        public GenericEventArgs(TData data)
        {
            this.Data = data;
        }
    }
}
