using InternetForum.DAL.Interfaces;

namespace InternetForum.BLL.Services
{
    public class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
