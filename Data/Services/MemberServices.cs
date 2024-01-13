
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static MudBlazor.Icons;

namespace DotNetCW.Data
{
    public class MemberServices
    {
        public List<Customer> GetMembers()
        {
            string path = Utils.GetMembersPath();

            if (!File.Exists(path))
            {
                return new List<Customer>();
            }
            var json = File.ReadAllText(path);

            return JsonSerializer.Deserialize<List<Customer>>(json);

        }

        public void SaveMembers(List<Customer> members)
        {
            string path = Utils.GetMembersPath();

            var json = JsonSerializer.Serialize(members);

            File.WriteAllText(path, json);
        }

        public Customer GetCustomerByPhoneNum(string phoneNum)
        {
            List<Customer> customers = GetMembers();
            Customer customer = customers.FirstOrDefault(c => c.PhoneNum == phoneNum);
            return customer;
        }

  
        public void AddMember(Customer _member)
        {

            List<Customer> members = GetMembers();

            members.Add(_member);

            SaveMembers(members);
        }

        public void UpdateOrderCount (string phoneNum)
        {
            List<Customer> members = GetMembers();

            Customer member = members.FirstOrDefault(m => m.PhoneNum == phoneNum);

            member.OrderCount++;

            SaveMembers(members);
        }

    }
}
