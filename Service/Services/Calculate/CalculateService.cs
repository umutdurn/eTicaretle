﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Calculate
{
    public class CalculateService
    {
        public CalculateModel CartCalculate(List<Cart> carts, Cargo? cargo, float? paymentPrice) {

            CalculateModel calculateCartList = new CalculateModel();
            List<Calculate> calcModel = new List<Calculate>();

            decimal subtotal = 0;
            decimal total = 0;

            foreach (var cart in carts)
            {
                decimal price = 0;

                if (cart.Product.DiscountPrice != null)
                {
                    price = Convert.ToDecimal(cart.Product.DiscountPrice);
                }
                else
                {
                    price = cart.Product.Price;
                }

                decimal subTotal = Math.Round(cart.Piece * price, 2);

                total += subTotal;

                calcModel.Add(new Calculate { Product = cart.Product, Piece = cart.Piece, CartId = cart.Id, Subtotal = subTotal });
            }

            total += Convert.ToDecimal(paymentPrice);

            calculateCartList.Calculate = calcModel;
            calculateCartList.CartTotal = total;

            if (cargo != null)
            {
                calculateCartList.Cargo = cargo.Price;
            }

            decimal generalTotal = (calculateCartList.CartTotal + calculateCartList.Cargo) - calculateCartList.Coupon;

            calculateCartList.GeneralTotal = generalTotal;

            return calculateCartList;
        
        }

        public List<CalculateInstallmentModel> InstallmentCalculate(List<Installment> installment, CalculateModel calculate) { 
        
            List<CalculateInstallmentModel> listCalculateInstalmentModel = new List<CalculateInstallmentModel>();

            foreach (var item in installment)
            {
                var totalInterest = Math.Round((calculate.GeneralTotal / 100) * Convert.ToDecimal(item.Interest) + calculate.GeneralTotal,2);

                var installmentPrice = Math.Round(totalInterest / item.Number, 2);

                listCalculateInstalmentModel.Add(new CalculateInstallmentModel { InstallmentPrice = installmentPrice, NumberOfInstallment = item.Number.ToString(), TotalPrice = totalInterest });
            }

            return listCalculateInstalmentModel;
        
        }
    }
}
