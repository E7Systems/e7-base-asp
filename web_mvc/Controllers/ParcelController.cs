using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Rsff.BusinessLayer;
using System.Linq.Dynamic;
using web_mvc.ViewModels;
using web_mvc.Infrastructure;

namespace web_mvc.Controllers
{
    public class ParcelController : Controller
    {

        private static readonly ILog m_logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private const int PROJECTS_PER_PAGE = 10;

        // GET: Parcel
        // Displays a page of parcels

        public ActionResult Index(int page = 1, string sortOrder = "Asc", string sortBy = "APN")
        {
            m_logger.DebugFormat("Entering Index Action (Get)");
            ParcelsBusinessLogic parcelsBusinessLogic = new ParcelsBusinessLogic();

            Tuple<List<Parcel>, int> pagedDataTuple = null;

            #region Sanity Check Query String Data
            //sanity check data on query string
            //check to see if anyone tampered with the sortBy query string
            //this can made more generic but this will do for now.
            string[] parcelColumnNames = {
                                       "ParcelID",
                                       "APN",
                                       "Street",
                                       "City",
                                       "State",
                                       "Zip"
                                   };
            if (!parcelColumnNames.Contains(sortBy))
            {
                m_logger.Debug("Query String Tampering Detected.  Setting sort by to to APN.");
                sortBy = "APN";
            }

            #endregion

            #region Get Data From Databasae

            //data comes back packed into a tuple to avoid 2 trips to db
            //1st item is page of data
            //2nd item is total number of records in table
            pagedDataTuple = parcelsBusinessLogic.GetParcelPage(page, PROJECTS_PER_PAGE);
            List<Parcel> pageOfProjects = pagedDataTuple.Item1;
            int totalProjectsRecordCount = pagedDataTuple.Item2;
            m_logger.DebugFormat("Current Page: {0}, Rows per page: {1}, totalProjectsRecordCount: {2}", page, PROJECTS_PER_PAGE, totalProjectsRecordCount);

            #endregion

            #region Sort Page of Data
            //sort data here, do not bother going back to db for a measly 10 projects per page
            m_logger.DebugFormat("sortOrder:'{0}' sortBy:'{1}'", sortOrder, sortBy);

            //check to see if anyone tampered with the sortOrder query string
            if (!"Asc Desc".Contains(sortOrder))
            {
                m_logger.Debug("Query String Tampering Detected.  Setting sort order to Ascending.");
                sortOrder = "Asc";
            }

            //sort as requested
            string sortExpression = String.Format("{0} {1}", sortBy, sortOrder);
            m_logger.DebugFormat("sortExpression: {0}", sortExpression);
            pageOfProjects = pageOfProjects.OrderBy<Parcel>(sortExpression).ToList();

            //invert sort order indicator (on UI header) for next pass
            sortOrder = sortOrder == "Desc" ? "Asc" : "Desc";
            #endregion

            #region Pass Data to View for Display
            ParcelIndex parcelIndex = new ParcelIndex();
            parcelIndex.SortOrder = sortOrder;

            //stuff page of Projects into a ViewModel
            parcelIndex.Parcels = new PagedData<Parcel>(pageOfProjects, totalProjectsRecordCount, page, PROJECTS_PER_PAGE);

            //send it to be displayed in the view
            return View(parcelIndex);
            #endregion

        }
    }
}