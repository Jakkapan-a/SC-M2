﻿using SC_M2_V2_00;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ImageClassification
{
    //public class FileUtils
    //{
    //    public static IEnumerable<(string imagePath, string label)> LoadImagesFromDirectory(
    //        string folder,
    //        bool useFolderNameasLabel)
    //    {
    //        IEnumerable<string> imagesPath = Directory
    //            .GetFiles(folder, "*", searchOption: SearchOption.AllDirectories)
    //            .Where(x => Path.GetExtension(x) == ".jpg" || Path.GetExtension(x) == ".png");

    //        return useFolderNameasLabel
    //            ? imagesPath.Select(imagePath => (imagePath, Directory.GetParent(imagePath).Name))
    //            : imagesPath.Select(imagePath =>
    //            {
    //                string label = Path.GetFileName(imagePath);
    //                for (int index = 0; index < label.Length; index++)
    //                {
    //                    if (!char.IsLetter(label[index]))
    //                    {
    //                        label = label.Substring(0, index);
    //                        break;
    //                    }
    //                }
    //                return (imagePath, label);
    //            });
    //    }

    //    public static IEnumerable<InMemoryImageData> LoadInMemoryImagesFromDirectory(
    //        string folder,
    //        bool useFolderNameAsLabel = true)
    //        => LoadImagesFromDirectory(folder, useFolderNameAsLabel)
    //            .Select(x => new InMemoryImageData(
    //                image: File.ReadAllBytes(x.imagePath),
    //                label: x.label,
    //                imageFileName: Path.GetFileName(x.imagePath)));

    //    public static string GetAbsolutePath(Assembly assembly, string relativePath)
    //    {
    //        string assemblyFolderPath = new FileInfo(assembly.Location).Directory.FullName;

    //        return Path.Combine(assemblyFolderPath, relativePath);
    //    }
    //}
}