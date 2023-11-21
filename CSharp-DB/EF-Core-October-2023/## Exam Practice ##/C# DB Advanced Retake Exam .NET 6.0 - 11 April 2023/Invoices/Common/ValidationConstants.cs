namespace Invoices.Common;

public static class ValidationConstants
{
    // Product
    public const int MIN_PRODUCT_NAME_LENGTH = 9;
    public const int MAX_PRODUCT_NAME_LENGTH = 30;

    public const double MIN_PRODUCT_PRICE = 5.00;
    public const double MAX_PRODUCT_PRICE = 1000.00;


    // Address
    public const int MIN_STREET_NAME_LENGTH = 10;
    public const int MAX_STREET_NAME_LENGTH = 20;

    public const int MIN_CITY_NAME_LENGTH = 5;
    public const int MAX_CITY_NAME_LENGTH = 15;

    public const int MIN_COUNTRY_NAME_LENGTH = 5;
    public const int MAX_COUNTRY_NAME_LENGTH = 15;

    // Invoice
    public const int MIN_INVOICE_NUMBER_NAME_LENGTH = 1000000000;
    public const int MAX_INVOICE_NUMBER_NAME_LENGTH = 1500000000;

    // Client
    public const int MIN_CLIENT_NAME_LENGTH = 10;
    public const int MAX_CLIENT_NAME_LENGTH = 25;

    public const int MIN_CLIENT_NUMBER_VAT_LENGTH = 10;
    public const int MAX_CLIENT_NUMBER_VAT_LENGTH = 15;
}