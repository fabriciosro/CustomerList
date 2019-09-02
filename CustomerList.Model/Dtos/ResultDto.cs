using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerList.Model.Dtos
{
  public class ResultDto
  {
    public Nullable<int> Id { get; set; }
    public bool Sucess { get; set; }
    public List<string> ListError { get; set; }

    public ResultDto()
    {
      this.ListError = new List<string>();
    }
  }
}
