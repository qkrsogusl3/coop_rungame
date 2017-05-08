﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class InApp : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    public static string mProductID_0 = "com.ryu.pokpoong.product_0";

    private bool mIsInitialized
    {
        get
        {
            return m_StoreController != null && m_StoreExtensionProvider != null;
        }
    }

    private void Start()
    {
        InitializePurchasing();

    }
    public void InitializePurchasing()
    {
        if (mIsInitialized)
        {
            // ... we are done here.
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(mProductID_0, ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
    }

    #region IStoreListener

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("InApp Initialize Failed \n" + error);
    }


    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.Log("Purchase Failed \n" + i.definition.id + "\n" + p);
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, mProductID_0, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }

        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }

    #endregion


    private void BuyProductID(string tProductID)
    {
        if(mIsInitialized)
        {
            Product tProduct = m_StoreController.products.WithID(tProductID);
            if(tProduct != null && tProduct.availableToPurchase)
            {

            }


        }
    }
    //void BuyProductID(string productId)
    //{
    //    // If Purchasing has been initialized ...
    //    if (IsInitialized())
    //    {
    //        // ... look up the Product reference with the general product identifier and the Purchasing 
    //        // system's products collection.
    //        Product product = m_StoreController.products.WithID(productId);

    //        // If the look up found a product for this device's store and that product is ready to be sold ... 
    //        if (product != null && product.availableToPurchase)
    //        {
    //            Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
    //            // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
    //            // asynchronously.
    //            m_StoreController.InitiatePurchase(product);
    //        }
    //        // Otherwise ...
    //        else
    //        {
    //            // ... report the product look-up failure situation  
    //            Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
    //        }
    //    }
    //    // Otherwise ...
    //    else
    //    {
    //        // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
    //        // retrying initiailization.
    //        Debug.Log("BuyProductID FAIL. Not initialized.");
    //    }
    //}


    //// Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    //// Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    //public void RestorePurchases()
    //{
    //    // If Purchasing has not yet been set up ...
    //    if (!IsInitialized())
    //    {
    //        // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
    //        Debug.Log("RestorePurchases FAIL. Not initialized.");
    //        return;
    //    }

    //    // If we are running on an Apple device ... 
    //    if (Application.platform == RuntimePlatform.IPhonePlayer ||
    //        Application.platform == RuntimePlatform.OSXPlayer)
    //    {
    //        // ... begin restoring purchases
    //        Debug.Log("RestorePurchases started ...");

    //        // Fetch the Apple store-specific subsystem.
    //        var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
    //        // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
    //        // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
    //        apple.RestoreTransactions((result) => {
    //            // The first phase of restoration. If no more responses are received on ProcessPurchase then 
    //            // no purchases are available to be restored.
    //            Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
    //        });
    //    }
    //    // Otherwise ...
    //    else
    //    {
    //        // We are not running on an Apple device. No work is necessary to restore purchases.
    //        Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
    //    }
    //}


    ////  
    //// --- IStoreListener
    ////

    //public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    //{
    //    // Purchasing has succeeded initializing. Collect our Purchasing references.
    //    Debug.Log("OnInitialized: PASS");

    //    // Overall Purchasing system, configured with products for this application.
    //    m_StoreController = controller;
    //    // Store specific subsystem, for accessing device-specific store features.
    //    m_StoreExtensionProvider = extensions;
    //}


    //public void OnInitializeFailed(InitializationFailureReason error)
    //{
    //    // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
    //    Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    //}


    //public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    //{
    //    if (String.Equals(args.purchasedProduct.definition.id, mProductID_0, StringComparison.Ordinal))
    //    {
    //        Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

    //        //ryu
    //        mMsg = "Purchasing is SUCCESSFULLY!!";
    //        mIsShowDx = true;

    //    }
    //    // Or ... an unknown product has been purchased by this user. Fill in additional products here....
    //    else
    //    {
    //        Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
    //    }

    //    // Return a flag indicating whether this product has completely been received, or if the application needs 
    //    // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
    //    // saving purchased products to the cloud, and when that save is delayed. 
    //    return PurchaseProcessingResult.Complete;
    //}


    //public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    //{
    //    // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
    //    // this reason with the user to guide their troubleshooting actions.
    //    Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));


    //    mMsg = "Purchasing is FAILED!!";
    //}




    //void OnGUI()
    //{
    //    if (true == mIsShowDx)
    //    {
    //        if (true == GUI.Button(new Rect(0, 250, 480, 100), mMsg))
    //        {
    //            mIsShowDx = false;
    //        }
    //    }
    //}
}
