using NetBarcode;
using NSubstitute;
using StoreManagementSystemX.Domain.Repositories.Products.Interfaces;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace StoreManagementSystemX.Tests
{
    public class BarcodeTests
    {

        private readonly ITestOutputHelper output;

        public BarcodeTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Barcode_has_length_13()
        {
            // assemble
            var productRepository = Substitute.For<IProductRepository>();
            var barcodeGeneratorService = new BarcodeGeneratorService(productRepository);

            // act
            var barcode = barcodeGeneratorService.GenerateBarcode();
            output.WriteLine("barcode: " + barcode);

            //assert
            Assert.Equal(13, barcode.Length);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(123)]
        [InlineData(43243)]
        [InlineData(12343)]
        [InlineData(0)]
        [InlineData(0000000000)]
        [InlineData(9999999999)]
        [InlineData(1111111111)]
        [InlineData(999999999)]
        public void String_has_length_10(Int64 value)
        {
            // assemble
            string numberString = value.ToString("D10");

            // act
            int length = numberString.Length;

            //assert
            output.WriteLine(numberString);
            Assert.Equal(10, length);
        }

    }
}
