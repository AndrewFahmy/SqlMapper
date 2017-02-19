using System.Data.Common;

namespace SqlMapper.Common
{
    internal class UnitofWork
    {
        internal DbTransaction TransactionInstance;

        internal bool UseTransaction => TransactionInstance != null;

        internal void CreateTransaction(DbConnection con)
        {
            if (TransactionInstance != null || con == null)
            {
                DisposeofTransaction();
                return;
            }

            TransactionInstance = con.BeginTransaction();
        }

        internal void Commit()
        {
            TransactionInstance?.Commit();

            DisposeofTransaction();
        }

        internal void Rollback()
        {
            TransactionInstance?.Rollback();

            DisposeofTransaction();
        }

        private void DisposeofTransaction()
        {
            TransactionInstance?.Dispose();
            TransactionInstance = null;
        }
    }
}