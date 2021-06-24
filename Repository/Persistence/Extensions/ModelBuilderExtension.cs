using Microsoft.EntityFrameworkCore;
using Repository.Persistence.Models;
using System.IO;

namespace Repository.Persistence.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<MachineLearningModel>().HasData(
                new MachineLearningModel
                {
                    Id = 1,
                    Name = "Super Resolution",
                    Architecture = "SRResNet",
                    Loss = "MSE"
                },
                new MachineLearningModel
                {
                    Id = 2,
                    Name = "Super Resolution",
                    Architecture = "SRGAN",
                    Loss = "MSE"
                },
                new MachineLearningModel
                {
                    Id = 3,
                    Name = "Super Resolution",
                    Architecture = "SRResNet",
                    Loss = "SSIM"
                }
            );
            builder.Entity<SuperResolutionModel>().HasData(
                new SuperResolutionModel
                {
                    ModelId = 1,
                    UpscaleFactor = 2,
                    Path = Path.Combine("Resources", "SuperResolution", "SRResNet", "MSE", "super_resolution_2x.onnx")
                },
                new SuperResolutionModel
                {
                    ModelId = 1,
                    UpscaleFactor = 4,
                    Path = Path.Combine("Resources", "SuperResolution", "SRResNet", "MSE", "super_resolution_4x.onnx")
                },
                new SuperResolutionModel
                {
                    ModelId = 1,
                    UpscaleFactor = 8,
                    Path = Path.Combine("Resources", "SuperResolution", "SRResNet", "MSE", "super_resolution_8x.onnx")
                },
                new SuperResolutionModel
                {
                    ModelId = 2,
                    UpscaleFactor = 2,
                    Path = Path.Combine("Resources", "SuperResolution", "SRGAN", "MSE", "super_resolution_2x.onnx")
                },
                new SuperResolutionModel
                {
                    ModelId = 2,
                    UpscaleFactor = 4,
                    Path = Path.Combine("Resources", "SuperResolution", "SRGAN", "MSE", "super_resolution_4x.onnx")
                },
                new SuperResolutionModel
                {
                    ModelId = 2,
                    UpscaleFactor = 8,
                    Path = Path.Combine("Resources", "SuperResolution", "SRGAN", "MSE", "super_resolution_8x.onnx")
                },
                new SuperResolutionModel
                {
                    ModelId = 3,
                    UpscaleFactor = 2,
                    Path = Path.Combine("Resources", "SuperResolution", "SRResNet", "SSIM", "super_resolution_2x.onnx")
                },
                new SuperResolutionModel
                {
                    ModelId = 3,
                    UpscaleFactor = 4,
                    Path = Path.Combine("Resources", "SuperResolution", "SRResNet", "SSIM", "super_resolution_4x.onnx")
                }
            );
        }
    }
}