using Microsoft.EntityFrameworkCore;
using Model.Persistence.Models;
using Model.Persistence.Models.Enums;
using System.IO;

namespace Model.Persistence.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SuperResolutionModel>().HasData(
               new SuperResolutionModel
               {
                   Id = 1,
                   Name = "Super Resolution SRResNET MSE",
                   UpscaleFactor = UpscaleFactor.x2,
                   Path = Path.Combine("MachineLearningModels", "SuperResolution_SRResNet_MSE", "super_resolution_2x.onnx")
               },
               new SuperResolutionModel
               {
                   Id = 2,
                   Name = "Super Resolution SRResNET MSE",
                   UpscaleFactor = UpscaleFactor.x4,
                   Path = Path.Combine("MachineLearningModels", "SuperResolution_SRResNet_MSE", "super_resolution_4x.onnx")
               },
               new SuperResolutionModel
               {
                   Id = 3,
                   Name = "Super Resolution SRResNET MSE",
                   UpscaleFactor = UpscaleFactor.x8,
                   Path = Path.Combine("MachineLearningModels", "SuperResolution_SRResNet_MSE", "super_resolution_8x.onnx")
               },
               new SuperResolutionModel
               {
                   Id = 4,
                   Name = "Super Resolution SRGAN MSE",
                   UpscaleFactor = UpscaleFactor.x2,
                   Path = Path.Combine("MachineLearningModels", "SuperResolution_SRGAN", "super_resolution_2x.onnx")
               },
               new SuperResolutionModel
               {
                   Id = 5,
                   Name = "Super Resolution SRGAN MSE",
                   UpscaleFactor = UpscaleFactor.x4,
                   Path = Path.Combine("MachineLearningModels", "SuperResolution_SRGAN", "super_resolution_4x.onnx")
               },
               new SuperResolutionModel
               {
                   Id = 6,
                   Name = "Super Resolution SRGAN MSE",
                   UpscaleFactor = UpscaleFactor.x8,
                   Path = Path.Combine("MachineLearningModels", "SuperResolution_SRGAN", "super_resolution_8x.onnx")
               },
               new SuperResolutionModel
               {
                   Id = 7,
                   Name = "Super Resolution SRResNET SSIM",
                   UpscaleFactor = UpscaleFactor.x2,
                   Path = Path.Combine("MachineLearningModels", "SuperResolution_SRResNet_SSIM", "super_resolution_2x.onnx")
               },
               new SuperResolutionModel
               {
                   Id = 8,
                   Name = "Super Resolution SRResNET SSIM",
                   UpscaleFactor = UpscaleFactor.x4,
                   Path = Path.Combine("MachineLearningModels", "SuperResolution_SRResNet_SSIM", "super_resolution_4x.onnx")
               }
           );
        }
    }
}