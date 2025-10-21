using System;

namespace Zano.Controllers;

public class WalletOpenException(string message) : Exception(message);