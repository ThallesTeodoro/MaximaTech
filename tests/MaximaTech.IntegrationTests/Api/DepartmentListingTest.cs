using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MaximaTech.Core.Entities;
using MaximaTech.Domain.Commands.Requests;
using Xunit;

namespace MaximaTech.IntegrationTests.Api
{
    public class DepartmentListingTest : IntegrationTestBase
    {
        [Fact]
        public async Task Index_WhenCalled_ReturnSuccess()
        {
            List<Department> departments = Department
                .Faker()
                .Generate(5);

            await AddManyEntitiesAsync<Department>(departments);

            List<Department> response = await SendAsync<List<Department>>(new DepartmentListingRequest());
            response.Should().HaveCount(c => c > 0);
        }
    }
}
