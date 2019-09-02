using CustomerList.Data.Repositories;
using CustomerList.Model.Interfaces;

namespace CustomerList.Data
{
  public class WorkStation : IWorkStation
  {
    public ApplicationContext Context { get; internal set; }

    private IUserRepository userRepository;

    public WorkStation(ApplicationContext context)
    {
      this.Context = context;
    }

    public IUserRepository UserRepository
    {
      get
      {
        if (this.userRepository == null)
          this.userRepository = new UserRepository(this.Context);
        return this.userRepository;
      }
    }

    public bool SaveChanges()
    {
      return this.Context.SaveChanges() > 0;
    }
  }
}
