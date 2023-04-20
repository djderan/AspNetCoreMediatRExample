using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RazorPagesLab.Pages.AddressBook
{
	public class DeleteAddressHandler : IRequestHandler<DeleteAddressRequest, Guid>
	{
		private readonly IRepo<AddressBookEntry> _repo;

		public DeleteAddressHandler(IRepo<AddressBookEntry> repo) 
		{
			_repo = repo;
		}

		public async Task<Guid> Handle(DeleteAddressRequest request, CancellationToken cancellationToken)
		{
			AddressBookEntry entry = _repo.Find(new EntryByIdSpecification(request.Id)).FirstOrDefault()
				?? throw new InvalidOperationException("Address Book Entry Not Found");
			_repo.Remove(entry);
			return await Task.FromResult(request.Id);
		}
	}
}
