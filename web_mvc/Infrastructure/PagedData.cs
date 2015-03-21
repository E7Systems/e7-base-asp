using log4net;
using System;
using System.Collections.Generic;

namespace web_mvc.Infrastructure
{
    //handles paging for all data bound views
    //centralizes error prone paging logic 
    //that usually ends up being cut/pasted into every view
    public class PagedData<T> : IEnumerable<T>
    {
        private static readonly ILog m_logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        private readonly IEnumerable<T> m_CurrentItems;

        public int TotalCount { get; private set; }
        public int Page { get; private set; }
        public int PerPage { get; private set; }
        public int TotalPages { get; private set; }

        public bool HasNextPage { get; private set; }
        public bool HasPreviousPage { get; private set; }

        public int NextPage
        {
            get
            {
                if (!this.HasNextPage)
                    throw new InvalidOperationException();

                return this.Page + 1;
            }
        }

        public int PreviousPage
        {
            get
            {
                if (!this.HasPreviousPage)
                    throw new InvalidOperationException();

                return this.Page - 1;
            }
        }

        public PagedData(IEnumerable<T> currentItems, int totalCount, int page, int perPage )
        {
            m_CurrentItems = currentItems;
            this.TotalCount = totalCount;
            this.Page = page;
            this.PerPage = perPage;

            this.TotalPages = Convert.ToInt32(Math.Ceiling((float)this.TotalCount / this.PerPage));

            this.HasNextPage = this.Page < this.TotalPages;
            this.HasPreviousPage = this.Page > 1;

            m_logger.DebugFormat("PagedData ctor for type:{0}.  Internal state: {1}", currentItems.GetType().ToString(), this.ToString());
        }

        public override string ToString()
        {
            return string.Format("TotalCount: {0}, Page: {1}, PerPage: {2}, TotalPages: {3}, HasNextPage: {4}, HasPreviousPage:{5}", 
                this.TotalCount, this.Page, this.PerPage, this.TotalPages, this.HasNextPage, this.HasPreviousPage);

        }


        public IEnumerator<T> GetEnumerator()
        {
            return m_CurrentItems.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}