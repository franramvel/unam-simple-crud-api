namespace Model.DB.Responses
{

    public record InternalResponse<T>(int HttpCode, string Message, T? Data);

}
