using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace FT.Repositories.NHibernate {
    public class RandomOrder : Order {
        public RandomOrder() : base("", true) { }

        public override SqlString ToSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery) {
            return new SqlString("newid()");
        }
    }
}