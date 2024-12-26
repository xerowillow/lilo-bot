public class MemberBirthdayModel {
    /// <summary>
    /// Id for the member recieving the birthday role.
    /// </summary>
    public ulong MemberId { get; private set; }

    /// <summary>
    /// Timestamp for when to remove the birthday role from a member.
    /// </summary>
    public ulong Expiration { get; private set; }

    public MemberBirthdayModel(ulong memberId, ulong expiration) {
        MemberId = memberId;
        Expiration = expiration;
    }
}