using System;

namespace Dianzhu.ApplicationService.JWT
{
	public class SignatureVerificationException : Exception
	{
		public SignatureVerificationException(string message)
			: base(message)
		{
		}
	}
}