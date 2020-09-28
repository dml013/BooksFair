using System;
using System.Collections.Generic;
using System.Text;
using Dapper;

namespace BooksFair.DataAccess.Repository.IRepository {
    public interface ISPCall : IDisposable { //I Store Procedure
        /// <summary>
        /// Return Single parametr
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedureName"></param>
        /// <param name="param"></param>
        /// <returns>Single parametr</returns>
        T Single<T>(string procedureName, DynamicParameters param = null);

        void Execute(string procedureName, object param = null);

        /// <summary>
        /// Returne one row
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedureName"></param>
        /// <param name="param"></param>
        /// <returns>one row</returns>
        T OneRecord<T>(string procedureName, DynamicParameters param = null);

        IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null);

        Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1,T2>(string procedureName, DynamicParameters param = null);
    }
}
