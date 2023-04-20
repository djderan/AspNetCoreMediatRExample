using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RazorPagesLab.Pages.AddressBook
{
    public class UpdateAddressHandler : IRequestHandler<UpdateAddressRequest, Guid>
    {
        private readonly IRepo<AddressBookEntry> _repo;

        public UpdateAddressHandler(IRepo<AddressBookEntry> repo)
        {
            _repo = repo;
        }

        public async Task<Guid> Handle(UpdateAddressRequest request, CancellationToken cancellationToken)
        {
            AddressBookEntry addressBookEntry = _repo.Find(new EntryByIdSpecification(request.Id)).FirstOrDefault()
                ?? throw new InvalidOperationException("Address Book Entry Not Found");
            addressBookEntry.Update(line1: request.Line1,
                line2: request.Line2,
                city: request.City,
                state: request.State,
                postalCode: request.PostalCode);
            _repo.Update(addressBookEntry);
            return await Task.FromResult(request.Id);
        }
    }
}
