using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpEmailLibrary
{
    public enum MailStatus
    {
        CLIENT_OFFER,
        CLIENT_OFFER_REJECTED,
        CLIENT_OFFER_ACCEPTED,
        ADDING_PRODUCTS_REQUEST,
        ADDING_PRODUCTS_RESPONSE,
        PRODUCTS_LIST_MERGED,
        PRODUCTS_LIST_ACCEPTED,
        RESPONSE_TO_CLIENT,
        RESPONSE_TO_SERVER,
        RESPONSE_TO_ARCHIVE,
        TEST
    }

    public static class MailsCredentials
    {
        public static string mainMailAddress = "ksiegarnia.longinus@gmail.com";
        public static string mainMailPassword = "Myszykiszki18";

        public static string mainMailStatusHeaderKey = "X-LonginusMailStatus";
    }
}
