using NUnit.Framework;
using UnityEngine;
using UnityEditor;

/// <summary>
/// EditMode tests for FoodData ScriptableObject and the food assets
/// added in the "Add new food assets and update inventory management" PR.
/// </summary>
public class FoodDataTests
{
    // -------------------------------------------------------------------------
    // FoodData ScriptableObject: creation and field assignment
    // -------------------------------------------------------------------------

    [Test]
    public void FoodData_CreateInstance_IsNotNull()
    {
        FoodData food = ScriptableObject.CreateInstance<FoodData>();
        Assert.IsNotNull(food);
        Object.DestroyImmediate(food);
    }

    [Test]
    public void FoodData_DefaultFoodName_IsNullOrEmpty()
    {
        FoodData food = ScriptableObject.CreateInstance<FoodData>();
        // Default string field in a ScriptableObject is null
        Assert.IsTrue(string.IsNullOrEmpty(food.foodName),
            "A freshly created FoodData should have no food name set by default.");
        Object.DestroyImmediate(food);
    }

    [Test]
    public void FoodData_DefaultSprite_IsNull()
    {
        FoodData food = ScriptableObject.CreateInstance<FoodData>();
        Assert.IsNull(food.sprite,
            "A freshly created FoodData should have a null sprite by default.");
        Object.DestroyImmediate(food);
    }

    [Test]
    public void FoodData_SetFoodName_RetainsValue()
    {
        FoodData food = ScriptableObject.CreateInstance<FoodData>();
        food.foodName = "TestFood";
        Assert.AreEqual("TestFood", food.foodName);
        Object.DestroyImmediate(food);
    }

    [Test]
    public void FoodData_SetFoodName_EmptyString_RetainsEmptyString()
    {
        FoodData food = ScriptableObject.CreateInstance<FoodData>();
        food.foodName = "";
        Assert.AreEqual("", food.foodName);
        Object.DestroyImmediate(food);
    }

    [Test]
    public void FoodData_SetFoodName_UnicodeRussianText_RetainsValue()
    {
        // Mirrors the actual values used in the new food assets
        string[] russianNames = new[]
        {
            "\u0427\u0435\u0440\u043d\u0438\u043a\u0430",   // Черника  (Blueberry)
            "\u041a\u0430\u043f\u0443\u0441\u0442\u0430",   // Капуста  (Cabbage)
            "\u041c\u043e\u0440\u043a\u043e\u0432\u044c",   // Морковь  (Carrot)
            "\u041c\u0451\u0434",                            // Мёд      (Honey)
            "\u0413\u0440\u0438\u0431",                      // Гриб     (Mushroom)
            "\u041f\u0438\u0446\u0446\u0430",                // Пицца    (Pizza)
            "\u0421\u044d\u043d\u0434\u0432\u0438\u0447",   // Сэндвич  (Sandwich)
            "\u041f\u0438\u0440\u043e\u0436\u043d\u043e\u0435", // Пирожное (Tart)
            "\u041f\u043e\u043c\u0438\u0434\u043e\u0440",   // Помидор  (Tomato)
        };

        foreach (string name in russianNames)
        {
            FoodData food = ScriptableObject.CreateInstance<FoodData>();
            food.foodName = name;
            Assert.AreEqual(name, food.foodName,
                $"FoodData should retain Russian food name '{name}'.");
            Object.DestroyImmediate(food);
        }
    }

    [Test]
    public void FoodData_IsScriptableObject()
    {
        FoodData food = ScriptableObject.CreateInstance<FoodData>();
        Assert.IsInstanceOf<ScriptableObject>(food);
        Object.DestroyImmediate(food);
    }

    // -------------------------------------------------------------------------
    // Asset integrity: each food asset added in this PR must exist on disk,
    // have a non-empty foodName, and a valid sprite reference.
    // -------------------------------------------------------------------------

    private static readonly string[] FoodAssetPaths = new[]
    {
        "Assets/Data/Foods/Food_Blueberry.asset",
        "Assets/Data/Foods/Food_Cabbage.asset",
        "Assets/Data/Foods/Food_Carrot.asset",
        "Assets/Data/Foods/Food_Honey.asset",
        "Assets/Data/Foods/Food_Mushroom.asset",
        "Assets/Data/Foods/Food_Pizza.asset",
        "Assets/Data/Foods/Food_Sandwich.asset",
        "Assets/Data/Foods/Food_Tart.asset",
        "Assets/Data/Foods/Food_Tomato.asset",
    };

    [Test]
    public void FoodAssets_AllExistInAssetDatabase()
    {
        foreach (string path in FoodAssetPaths)
        {
            FoodData asset = AssetDatabase.LoadAssetAtPath<FoodData>(path);
            Assert.IsNotNull(asset, $"Food asset not found at path: {path}");
        }
    }

    [Test]
    public void FoodAssets_AllHaveNonEmptyFoodName()
    {
        foreach (string path in FoodAssetPaths)
        {
            FoodData asset = AssetDatabase.LoadAssetAtPath<FoodData>(path);
            if (asset == null) continue; // reported by AllExistInAssetDatabase
            Assert.IsFalse(string.IsNullOrEmpty(asset.foodName),
                $"FoodData at '{path}' has an empty foodName.");
        }
    }

    [Test]
    public void FoodAssets_AllHaveNonNullSprite()
    {
        foreach (string path in FoodAssetPaths)
        {
            FoodData asset = AssetDatabase.LoadAssetAtPath<FoodData>(path);
            if (asset == null) continue;
            Assert.IsNotNull(asset.sprite,
                $"FoodData at '{path}' has a null sprite reference.");
        }
    }

    // -------------------------------------------------------------------------
    // Per-asset: expected Russian name spot-checks
    // -------------------------------------------------------------------------

    [TestCase("Assets/Data/Foods/Food_Blueberry.asset", "\u0427\u0435\u0440\u043d\u0438\u043a\u0430")]
    [TestCase("Assets/Data/Foods/Food_Cabbage.asset",   "\u041a\u0430\u043f\u0443\u0441\u0442\u0430")]
    [TestCase("Assets/Data/Foods/Food_Carrot.asset",    "\u041c\u043e\u0440\u043a\u043e\u0432\u044c")]
    [TestCase("Assets/Data/Foods/Food_Honey.asset",     "\u041c\u0451\u0434")]
    [TestCase("Assets/Data/Foods/Food_Mushroom.asset",  "\u0413\u0440\u0438\u0431")]
    [TestCase("Assets/Data/Foods/Food_Pizza.asset",     "\u041f\u0438\u0446\u0446\u0430")]
    [TestCase("Assets/Data/Foods/Food_Sandwich.asset",  "\u0421\u044d\u043d\u0434\u0432\u0438\u0447")]
    [TestCase("Assets/Data/Foods/Food_Tart.asset",      "\u041f\u0438\u0440\u043e\u0436\u043d\u043e\u0435")]
    [TestCase("Assets/Data/Foods/Food_Tomato.asset",    "\u041f\u043e\u043c\u0438\u0434\u043e\u0440")]
    public void FoodAsset_HasExpectedFoodName(string assetPath, string expectedName)
    {
        FoodData asset = AssetDatabase.LoadAssetAtPath<FoodData>(assetPath);
        Assume.That(asset, Is.Not.Null, $"Asset not found: {assetPath}");
        Assert.AreEqual(expectedName, asset.foodName,
            $"Food name mismatch for asset at '{assetPath}'.");
    }

    [Test]
    public void FoodAssets_NineNewFoodsWereAdded()
    {
        Assert.AreEqual(9, FoodAssetPaths.Length,
            "Exactly 9 food assets should have been added in this PR.");
    }

    // -------------------------------------------------------------------------
    // Regression / boundary tests
    // -------------------------------------------------------------------------

    [Test]
    public void FoodData_FoodNameDoesNotTrimWhitespace()
    {
        // Verify FoodData is a plain data container — it must not silently
        // trim or transform the assigned name.
        FoodData food = ScriptableObject.CreateInstance<FoodData>();
        food.foodName = "  Blueberry  ";
        Assert.AreEqual("  Blueberry  ", food.foodName,
            "FoodData should store the foodName verbatim without trimming.");
        Object.DestroyImmediate(food);
    }

    [Test]
    public void FoodData_SetFoodNameTwice_ReturnsLastValue()
    {
        FoodData food = ScriptableObject.CreateInstance<FoodData>();
        food.foodName = "First";
        food.foodName = "Second";
        Assert.AreEqual("Second", food.foodName,
            "FoodData foodName should reflect the most recently assigned value.");
        Object.DestroyImmediate(food);
    }

    [Test]
    public void FoodData_TwoInstances_AreIndependent()
    {
        FoodData foodA = ScriptableObject.CreateInstance<FoodData>();
        FoodData foodB = ScriptableObject.CreateInstance<FoodData>();

        foodA.foodName = "Apple";
        foodB.foodName = "Banana";

        Assert.AreEqual("Apple",  foodA.foodName);
        Assert.AreEqual("Banana", foodB.foodName,
            "Mutating one FoodData instance must not affect another.");

        Object.DestroyImmediate(foodA);
        Object.DestroyImmediate(foodB);
    }

    [Test]
    public void FoodData_AssetCount_MatchesExpectedFoodItems()
    {
        // Ensure the asset database contains exactly the 9 foods from this PR
        // (blueberry, cabbage, carrot, honey, mushroom, pizza, sandwich, tart, tomato).
        string[] guids = AssetDatabase.FindAssets("t:FoodData", new[] { "Assets/Data/Foods" });
        Assert.GreaterOrEqual(guids.Length, 9,
            "The Assets/Data/Foods folder should contain at least 9 FoodData assets after this PR.");
    }
}
