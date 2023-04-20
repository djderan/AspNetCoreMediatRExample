using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesLab.Pages.AddressBook
{
    public class DeleteModel : PageModel
    {
		private readonly IMediator _mediator;
		private readonly IRepo<AddressBookEntry> _repo;

		public DeleteModel(IRepo<AddressBookEntry> repo, IMediator mediator)
		{
			_repo = repo;
			_mediator = mediator;
		}

		[BindProperty]
		public DeleteAddressRequest DeleteAddressRequest { get; set; }

		public void OnGet(Guid id)
		{
			IEnumerable<AddressBookEntry> addressBookEntries = _repo.Find(new EntryByIdSpecification(id));

			AddressBookEntry addressBookEntry = addressBookEntries.FirstOrDefault(a => a.Id.Equals(id));

			if (addressBookEntry == null)
			{
				return;
			}

			DeleteAddressRequest = new DeleteAddressRequest
			{
				Id = addressBookEntry.Id,
				Line1 = addressBookEntry.Line1,
				Line2 = addressBookEntry.Line2,
				City = addressBookEntry.City,
				PostalCode = addressBookEntry.PostalCode,
				State = addressBookEntry.State
			};
		}

		public async Task<ActionResult> OnPostDelete()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			_ = await _mediator.Send(DeleteAddressRequest);
			return RedirectToPage("Index");
		}

		public ActionResult OnPostCancel()
		{
			if (!ModelState.IsValid) 
			{
				return Page();
			}
			return RedirectToPage("Index");
		}
	}
}
