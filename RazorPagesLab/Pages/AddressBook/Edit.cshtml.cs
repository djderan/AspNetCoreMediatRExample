using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages.AddressBook;

public class EditModel : PageModel
{
	private readonly IMediator _mediator;
	private readonly IRepo<AddressBookEntry> _repo;

	public EditModel(IRepo<AddressBookEntry> repo, IMediator mediator)
	{
		_repo = repo;
		_mediator = mediator;
	}

	[BindProperty]
	public UpdateAddressRequest UpdateAddressRequest { get; set; }

	public void OnGet(Guid id)
	{
		// Todo: Use repo to get address book entry, set UpdateAddressRequest fields.

		IEnumerable<AddressBookEntry> addressBookEntries = _repo.Find(new EntryByIdSpecification(id));

		AddressBookEntry addressBookEntry = addressBookEntries.FirstOrDefault(a => a.Id.Equals(id));

		if (addressBookEntry == null) 
		{
			return;
		}

        UpdateAddressRequest = new UpdateAddressRequest
		{
			Id = addressBookEntry.Id,
			Line1 = addressBookEntry.Line1,
			Line2 = addressBookEntry.Line2,
			City = addressBookEntry.City,
			PostalCode = addressBookEntry.PostalCode,
			State = addressBookEntry.State
		};
	}

	public async Task<ActionResult> OnPost()
	{
		// Todo: Use mediator to send a "command" to update the address book entry, redirect to entry list.
		if(!ModelState.IsValid)
		{
			return Page();
		}
		_ = await _mediator.Send(UpdateAddressRequest);
		return RedirectToPage("Index");
	}
}