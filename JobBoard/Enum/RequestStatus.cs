namespace JobBoard.Enum
{

    public enum RequestStatus
    {
        Success,            // On Successful Processing of a Request
        EntryAlreadyExist,  // On Existence of an Entry Request
        FatalError,         // On Server Error Response of an Entry Request
        NoEntryFound,       // On Null Set Response of a Request
        InvalidRequest,     // On Invalid Data Entry sent to the Server for Processing
        BarredRequest       // On Impossible Server Processing: This is sent when there is a request the system cannot make on its own due to how delicate the request can be.
    }
}
