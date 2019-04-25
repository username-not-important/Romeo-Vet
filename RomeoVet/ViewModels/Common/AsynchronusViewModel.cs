using System;
using System.Threading.Tasks;
using Assisticant.Fields;

namespace RomeoVet.ViewModels.Common
{
    public class AsynchronusViewModel
    {
        private Observable<bool> _isBusy = new Observable<bool>(default(bool));
        private Observable<Exception> _lastException = new Observable<Exception>(default(Exception));

        private int _requestId;

        public bool HasError => _lastException.Value != null;

        public string Error => _lastException.Value?.Message;

        public bool IsBusy => _isBusy;
        public bool IsIdle => !_isBusy;

        protected async void Perform(Func<Task> work)
        {
            int myid = ++_requestId;

            try
            {
                _lastException.Value = null;
                _isBusy.Value = true;

                await Task.Delay(500);

                await work();
            }
            catch (Exception ex)
            {
                _lastException.Value = ex;
            }
            finally
            {
                if (_requestId == myid)
                    _isBusy.Value = false;
            }
        }

        protected void setBusy(bool value) => _isBusy.Value = value;
    }
}
