using NetBarcode;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoreManagementSystemX.Services
{
    public class BarcodeGeneratorService : IBarcodeGeneratorService
    {
        private static readonly Random _random = new Random();
        private static readonly string BUSINESS_PREFIX = "6390";
        private readonly IProductRepository _productRepository;

        // this service has a uses the EAN-13 which is a 13-digit format
        //      consisting of 12 numeric character + 1 error check bit
        public BarcodeGeneratorService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        private string GenerateTwelveDigitString()
        {
            return BUSINESS_PREFIX + _random.NextInt64(0, 99999999).ToString("D8");
        }

        public string GenerateBarcode()
        {
            // generates only 8 digits because we already have the 4 digit prefix

            var twelveDigitString = GenerateTwelveDigitString();
            var checkDigit = GenerateCheckDigit(twelveDigitString);
            var barcodeString = twelveDigitString + checkDigit;

            if(!IsUnique(barcodeString))
            {
                throw new Exception("Barcode is not unique. Please generate another barcode");
            }

            return barcodeString;
        }

        private bool IsUnique(string barcode)
        {
            // check for barcode collision
            var matchedProduct = _productRepository.GetByBarcode(barcode);

            return matchedProduct == null;
        }

        private string GenerateCheckDigit(string barcodeString)
        {
            int oddPositionedDigitsSum = 0;
            int evenPositionedDigitsSum = 0;
            for(int i = 0; i < barcodeString.Length; i++)
            {
                // multiply by 3 on even digit position
                int number = Int32.Parse(barcodeString[i].ToString()); // to string method call is important because character converts to ascii equivalent integer
                if((i+1) % 2 == 0)
                {
                    evenPositionedDigitsSum += number;
                } else
                {
                    oddPositionedDigitsSum += number;
                }
            }

            var sum = oddPositionedDigitsSum + (evenPositionedDigitsSum * 3);

            int remainder = sum % 10;

            if (remainder != 0)
            {
                return (10 - remainder).ToString();
            }

            return "0";
        }
    }
}
