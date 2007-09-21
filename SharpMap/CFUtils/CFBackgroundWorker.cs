using System;
using System.Collections.Generic;
using System.Text;

//translation from Daniel Moths implementation of the BackgroundWorker class
//available in .NET 2 but not in the compact framework


namespace System.ComponentModel {
    #region "EventArgs classes"

    #region "background worker"

    public class CFBackgroundWorker: ComponentModel.Component {
        public event DoWorkEventHandler DoWork;
        public event ProgressChangedEventHandler ProgressChanged;
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;

        public CFBackgroundWorker()
            : this(new Windows.Forms.Control())
        {
        }

        public CFBackgroundWorker(Windows.Forms.Control aControl)
            : base()
        {
            mGuiMarshaller = aControl;
        }

        public bool CancellationPending
        {
            get
            {
                return mCancelPending;
            }
        }

        public void ReportProgress(Int32 aProgressPercent)
        {
            this.ReportProgress(aProgressPercent, null);
        }

        public void ReportProgress(Int32 aProgressPercent, object aUserState)
        {
            if (!mDoesProgress)
            {
                throw new InvalidOperationException("doesn\'t do progress events. Ypu must use WorkerReportsProgress = true");
            }
            Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(ProgressHelper), new ProgressChangedEventArgs(aProgressPercent, aUserState));
        }
        public void RunWorkerAsync()
        {
            this.RunWorkerAsync(null);
        }
        public void RunWorkerAsync(object aArgument)
        {
            if (mInUse)
            {
                throw new InvalidOperationException("Already in use");
            }
            if (DoWork == null)
            {
                throw new InvalidOperationException("you must subscribe to the DoWorkEvent");
            }

            mInUse = true;
            mCancelPending = false;

            Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(DoTheRealWork), aArgument);
        }

        public void CancelAsync()
        {
            if (!mDoesCancel)
            {
                throw new InvalidOperationException("Doesn't support cancel, You must use WorkerSupportsCancellation=true");
            }
            mCancelPending = true;
        }

        public bool WorkerReportsProgress
        {
            get
            {
                return mDoesProgress;
            }
            set
            {
                mDoesProgress = value;
            }
        }

        public bool WorkerSupportsCancellation
        {
            get
            {
                return mDoesCancel;
            }
            set
            {
                mDoesCancel = value;
            }
        }

        public bool InProgress
        {
            get
            {
                return mInUse;
            }
        }

        #region "fields"
        private bool mInUse;
        private bool mCancelPending;
        private bool mDoesCancel;
        private bool mDoesProgress;
        private RunWorkerCompletedEventArgs mFinalResult;
        private ProgressChangedEventArgs mProgressArgs;
        private Windows.Forms.Control mGuiMarshaller;
        #endregion

        #region "private methods"

        private void ProgressHelper(object o)
        {
            mProgressArgs = o as ProgressChangedEventArgs;
            mGuiMarshaller.Invoke(new EventHandler(TellThemOnGuiProgress));
        }

        private void TellThemOnGuiProgress(object sender, EventArgs e)
        {
            this.ProgressChanged(this, mProgressArgs);
        }

        private void DoTheRealWork(object o)
        {
            Exception er = null;
            bool ca = false;
            object result = null;

            try
            {
                DoWorkEventArgs inOut = new DoWorkEventArgs(o);
                this.DoWork(this, inOut);
                result = inOut.Result;
            }
            catch (Exception ex)
            {
                er = ex;
            }

            RunWorkerCompletedEventArgs tempResult = new RunWorkerCompletedEventArgs(result, er, ca);
            Threading.ThreadPool.QueueUserWorkItem(new Threading.WaitCallback(RealWorkHelper), tempResult);
            mInUse = false;
            mCancelPending = false;
        }

        private void RealWorkHelper(object o)
        {
            mFinalResult = o as RunWorkerCompletedEventArgs;
            mGuiMarshaller.Invoke(new EventHandler(TellThemOnGuiCompleted));
        }

        private void TellThemOnGuiCompleted(object sender, EventArgs e)
        {
            this.RunWorkerCompleted(this, mFinalResult);
        }


        #endregion

    }

    #endregion

    public class RunWorkerCompletedEventArgs
    : EventArgs {
        private readonly object mResult;
        private readonly bool mCancelled;
        private readonly Exception mError;

        public RunWorkerCompletedEventArgs(object aResult, Exception aError, bool aCancelled)
        {
            mResult = aResult;
            mCancelled = aCancelled;
            mError = aError;
        }

        public Object Result
        {
            get
            {
                return mResult;
            }
        }

        public bool Cancelled
        {
            get
            {
                return mCancelled;
            }
        }
        public Exception Error
        {
            get
            {
                return mError;
            }
        }
    }

    public class ProgressChangedEventArgs
    : EventArgs {
        private readonly int mProgressPercent;
        private readonly object mUserState;
        public ProgressChangedEventArgs(Int32 aProgressPercent, object aUserState)
        {
            mProgressPercent = aProgressPercent;
            mUserState = aUserState;
        }

        public Int32 ProgressPercentage
        {
            get
            {
                return mProgressPercent;
            }
        }
        public object UserState
        {
            get
            {
                return mUserState;
            }
        }
    }

    public class DoWorkEventArgs
    : ComponentModel.CancelEventArgs {
        private readonly object mArgument;
        private object mResult;
        public DoWorkEventArgs(object aArgument)
        {
            mArgument = aArgument;
        }
        public object Argument
        {
            get
            {
                return mArgument;
            }
        }
        public object Result
        {
            get
            {
                return mResult;
            }
            set
            {
                mResult = value;
            }
        }
    }

    #endregion

    #region "delegates for 3 events of class"

    public delegate void DoWorkEventHandler(object sender, DoWorkEventArgs e);
    public delegate void ProgressChangedEventHandler(object sender, ProgressChangedEventArgs e);
    public delegate void RunWorkerCompletedEventHandler(object sender, RunWorkerCompletedEventArgs e);


    #endregion
}