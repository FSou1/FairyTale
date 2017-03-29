using System;
using NHibernate;
using NHibernate.SqlCommand;

namespace FT.Repositories.NHibernate {
    public class SqlInterceptor : EmptyInterceptor {
        private readonly Action<string> _log;

        public SqlInterceptor(Action<string> log) {
            _log = log;
        }

        public override SqlString OnPrepareStatement(SqlString sql) {
            _log.Invoke(sql.ToString());

            return base.OnPrepareStatement(sql);
        }
    }
}