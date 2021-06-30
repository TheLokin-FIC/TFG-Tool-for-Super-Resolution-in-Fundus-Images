using Microsoft.EntityFrameworkCore;
using Repository.Persistence.Models;
using System;
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
                    Loss = "MSE",
                    ShortDescription = "Clarify images and enhance resolution without feature loss with super resolution powered by an SRResNet architecture and MSE loss.",
                    LongDescription = "Most of the images are composed of many pixels. To enlarge an image, instead of simply duplicating the pixels and worsening image quality this implementation uses deep learning to clarify, sharpen and upscale images without losing its content and defining characteristics.",
                    CreationDate = new DateTime(2021, 6, 30)
                },
                new MachineLearningModel
                {
                    Id = 2,
                    Name = "Super Resolution",
                    Architecture = "SRGAN",
                    Loss = "MSE",
                    ShortDescription = "Clarify images and enhance resolution without feature loss with super resolution powered by an SRGAN architecture and MSE loss.",
                    LongDescription = "Most of the images are composed of many pixels. To enlarge an image, instead of simply duplicating the pixels and worsening image quality this implementation uses deep learning to clarify, sharpen and upscale images without losing its content and defining characteristics.",
                    CreationDate = new DateTime(2021, 6, 30)
                },
                new MachineLearningModel
                {
                    Id = 3,
                    Name = "Super Resolution",
                    Architecture = "SRResNet",
                    Loss = "SSIM",
                    ShortDescription = "Clarify images and enhance resolution without feature loss with super resolution powered by an SRResNet architecture and SSIM loss.",
                    LongDescription = "Most of the images are composed of many pixels. To enlarge an image, instead of simply duplicating the pixels and worsening image quality this implementation uses deep learning to clarify, sharpen and upscale images without losing its content and defining characteristics.",
                    CreationDate = new DateTime(2021, 6, 30)
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