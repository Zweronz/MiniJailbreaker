using System.Runtime.InteropServices;

public class IAPPlugin
{
	public enum Status
	{
		kUserCancel = -2,
		kError = -1,
		kBuying = 0,
		kSuccess = 1
	}

	[DllImport("__Internal")]
	protected static extern void PurchaseProduct(string productId, string productCount);

	[DllImport("__Internal")]
	protected static extern int PurchaseStatus();

	public static void NowPurchaseProduct(string productId, string productCount)
	{
		PurchaseProduct(productId, productCount);
	}

	public static Status GetPurchaseStatus()
	{
		int num = 1;
		return (Status)PurchaseStatus();
	}
}
