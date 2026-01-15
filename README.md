# BTCPay Server: Zano Plugin

This plugin extends BTCPay Server to enable users to receive payments via Zano.

## Overview

The Zano plugin for BTCPay Server provides comprehensive support for Zano cryptocurrency payments, including real-time blockchain monitoring, automated payment processing, and multi-network support. The plugin integrates seamlessly with BTCPay Server's existing architecture and provides a robust payment gateway for Zano transactions.

## Features

- **Real-time Payment Processing**: Automated detection and processing of Zano payments
- **Multi-Network Support**: Support for mainnet and testnet configurations
- **Advanced Confirmation Logic**: Configurable confirmation thresholds with speed policies
- **Event-Driven Architecture**: Seamless integration with BTCPay Server's event system
- **RPC Integration**: Direct communication with Zano daemon and wallet nodes
- **Blockchain Monitoring**: Configurable polling intervals for new block detection
- **Multi-Address Support**: Generates and manages multiple payment addresses per invoice

## Configuration

Configure this plugin using the following environment variables:

| Environment variable                       | Description                                                                                                                                                                                                                                   | Example                          |
| ------------------------------------------ | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | -------------------------------- |
| **BTCPAY_ZANO_DAEMON_URI**                | **Required**. The URI of the Zano daemon RPC interface.                                                                                                                                                                                       | http://127.0.0.1:32348           |
| **BTCPAY_ZANO_DAEMON_USERNAME**           | **Optional**. The username for authenticating with the daemon.                                                                                                                                                                                | username                         |
| **BTCPAY_ZANO_DAEMON_PASSWORD**           | **Optional**. The password for authenticating with the daemon.                                                                                                                                                                                | password                         |
| **BTCPAY_ZANO_WALLET_DAEMON_URI**         | **Required**. The URI of the Zano wallet RPC interface.                                                                                                                                                                                    | http://127.0.0.1:32349           |
| **BTCPAY_ZANO_WALLET_DAEMON_WALLETDIR**   | **Optional**. The directory where BTCPay Server saves wallet files uploaded via the UI.                                                                                                                                                       | /home/user/zano/wallets/         |
| **BTCPAY_ZANO_CASHCOW_WALLET_DAEMON_URI** | **Optional**. The URI of the Zano wallet RPC interface for the cashcow wallet. This is used to create a second wallet for testing purposes in regtest mode.                    | http://127.0.0.1:32350           |

## Installation

### Docker Deployment

BTCPay Server's Docker deployment simplifies the setup by automatically configuring these variables. For further details, refer to the [BTCPay Server documentation](https://docs.btcpayserver.org/).

### Manual Installation

1. **Build the Plugin**
   ```bash
   cd Plugins/Zano
   dotnet build
   ```

2. **Deploy to BTCPay Server**
   ```bash
   # Copy the built plugin to BTCPay Server plugins directory
   cp -r bin/Debug/net8.0/* /path/to/btcpayserver/plugins/Zano/
   ```

3. **Restart BTCPay Server**
   ```bash
   # Restart BTCPay Server to load the plugin
   sudo systemctl restart btcpayserver
   ```

## For Maintainers

### Building and Testing

### Local Development Setup

If you're contributing to this plugin or running a local development instance of BTCPay Server with the Zano plugin, follow these steps.

#### 1. Requirements

* .NET 8.0 SDK or later
* JetBrains Rider (recommended) or Visual Studio Code with C# support
* Git
* Docker and Docker Compose

#### 2. Clone the Repositories

Create a working directory and clone both the BTCPay Server and Zano plugin repositories side by side:

```bash
git clone https://github.com/btcpayserver/btcpayserver
git clone --recurse-submodules https://github.com/your-username/btcpayserver-zano-plugin
```

#### 3. Build the Plugin

Navigate to the plugin directory and restore/build the solution:

```bash
cd btcpayserver-zano-plugin
dotnet restore
dotnet build btcpay-zano-plugin.sln
```

To build and run unit tests, run the following commands:

```bash
dotnet build btcpay-zano-plugin.sln
dotnet test BTCPayServer.Plugins.UnitTests --verbosity normal
```

To run unit tests with coverage, install JetBrains dotCover CLI:

```bash
dotnet tool install --global JetBrains.dotCover.CommandLineTools
```

Then run the following command:

```bash
dotCover cover-dotnet --TargetArguments="test BTCPayServer.Plugins.UnitTests --no-build" --ReportType=HTML --Output=coverage/dotCover.UnitTests.output.html --ReportType=detailedXML --Output=coverage/dotCover.UnitTests.output.xml --filters="-:Assembly=BTCPayServer.Plugins.UnitTests;-:Assembly=testhost;-:Assembly=BTCPayServer;-:Class=AspNetCoreGeneratedDocument.*"
```

To build and run integration tests, run the following commands:

```bash
dotnet build btcpay-zano-plugin.sln
docker compose -f BTCPayServer.Plugins.IntegrationTests/docker-compose.yml run tests
```

#### 4. Configure BTCPay Server to Load the Plugin

For vscode, open the `launch.json` file in the `.vscode` folder and set the `launchSettingsProfile` to `Altcoins-HTTPS`.

Then create the `appsettings.dev.json` file in `btcpayserver/BTCPayServer`, with the following content:

```json
{
  "DEBUG_PLUGINS": "..\\..\\Plugins\\Zano\\bin\\Debug\\net8.0\\BTCPayServer.Plugins.Zano.dll",
  "ZANO_DAEMON_URI": "http://127.0.0.1:32348",
  "ZANO_WALLET_DAEMON_URI": "http://127.0.0.1:32349",
  "ZANO_CASHCOW_WALLET_DAEMON_URI": "http://127.0.0.1:32350"
}
```

This will ensure that BTCPay Server loads the plugin when it starts.

#### 5. Start Development Environment

Then start the development dependencies via docker-compose:

```bash
cd BTCPayServer.Plugins.IntegrationTests/
docker-compose up -d dev
```

Finally, set up BTCPay Server as the startup project in Rider or Visual Studio.

If you want to reset the environment you can run:

```bash
docker-compose down -v
docker-compose up -d dev
```

**Note**: Running or compiling the BTCPay Server project will not automatically recompile the plugin project. Therefore, if you make any changes to the project, do not forget to build it before running BTCPay Server in debug mode.

We recommend using Rider for plugin development, as it supports hot reload with plugins. You can edit `.cshtml` files, save, and refresh the page to see the changes.

Visual Studio does not support this feature.

When debugging in regtest, BTCPay Server will automatically create and configure two wallets. (cashcow and merchant) You can trigger payments or mine blocks on the invoice's checkout page.

### Code Formatting

We use the **unmodified** standardized `.editorconfig` from .NET SDK. Run `dotnet new editorconfig --force` to apply the latest version.

To enforce formatting for the whole project, run:

```bash
dotnet format btcpay-zano-plugin.sln --exclude submodules/* --verbosity diagnostic
```

To enforce custom analyzer configuration options, we do use global _AnalyzerConfig_ `.globalconfig` file.

## About Docker Compose Deployment

BTCPay Server maintains its own deployment stack project to enable users to easily update or deploy additional infrastructure (such as nodes).

Zano nodes are defined in this Docker Compose file.

To include Zano in your BTCPay deployment, you must add the Zano configuration to your Docker compose generator:
```bash
cd /opt/app/btcpayserver-docker/docker-compose-generator/docker-fragments
```
Upload zano.yml to docker-fragments
<img width="940" height="132" alt="image" src="https://github.com/user-attachments/assets/9283c60a-79bc-4ae8-9e16-1e9a6900e597" />

Go to /opt/app/btcpay_datadir

Make a new folder “ZanoWallet”

```bash
mkdir "ZanoWallet"
```
Upload “start-zano-wallet.sh” to ZanoWallet folder

Users can install Zano by configuring the `BTCPAYGEN_CRYPTOX` and `BTCPAYGEN_ADDITIONAL_FRAGMENTS` environment variables.

For example, after ensuring `BTCPAYGEN_CRYPTO2` is not already assigned to another cryptocurrency:

```bash
BTCPAYGEN_CRYPTO2="zano"
BTCPAYGEN_ADDITIONAL_FRAGMENTS=zano.yml
```
Execute command “. btcpay-setup.sh -i”
<img width="940" height="598" alt="image" src="https://github.com/user-attachments/assets/8054000c-ab73-43f7-aae8-8d5fcaf36cbf" />

This will automatically configure Zano in their deployment stack. Users can then run `btcpay-update.sh` to pull updates for the infrastructure.

<img width="940" height="564" alt="image" src="https://github.com/user-attachments/assets/2cf3cfc4-ac69-4d8b-9f32-746853b60b5d" />

After `btcpay-update.sh` execute the following command

```bash
docker compose -f Generated/docker-compose.generated.yml up -d zano-wallet-rpc
```
At this time all the required containers should be up and running.

Go to UI interface 
<img width="940" height="685" alt="image" src="https://github.com/user-attachments/assets/4eb0f53f-ac23-417c-a1ad-957808cb4eeb" />

Search for Zano and install the plugin
<img width="940" height="697" alt="image" src="https://github.com/user-attachments/assets/fcf2a18a-2992-43d4-afaf-f7008418a448" />

After restarting, you should see
<img width="940" height="430" alt="image" src="https://github.com/user-attachments/assets/d71fde56-1b93-426b-a0ed-f9c8c29a03f6" />

Go to “zano” menu after “settings” and click “modify”
<img width="940" height="255" alt="image" src="https://github.com/user-attachments/assets/f137ca1a-47cf-4755-89ac-13edea52c17e" />

Set the auditable wallet address and password
<img width="939" height="737" alt="image" src="https://github.com/user-attachments/assets/29e908d0-7a92-4b96-8956-15b36eb60899" />

Upon successful setting, you should see 
<img width="1061" height="548" alt="image" src="https://github.com/user-attachments/assets/67cb94fd-124a-44bf-8783-8a7bb6b01734" />

**Note**: Adding Zano to the infrastructure is not recommended for non-advanced users. If the server specifications are insufficient, it may become unresponsive.

## Architecture

### Core Components

#### ZanoListener
The main service that monitors the blockchain and processes payments:
- **Block Polling**: Timer-based polling every 3 seconds (configurable)
- **Event Processing**: Handles Zano blockchain events
- **Payment Updates**: Automatically updates payment states and confirmations
- **Invoice Activation**: Activates payment methods when sufficient confirmations are received

#### ZanoRPCProvider
Manages RPC connections to Zano nodes:
- **Daemon RPC**: Block height and network information
- **Wallet RPC**: Transaction details and transfer information
- **Connection Management**: Handles multiple network connections

#### Payment Handlers
Process Zano-specific payment data:
- **Payment Parsing**: Converts blockchain data to payment entities
- **Status Management**: Determines payment status based on confirmations
- **Amount Conversion**: Handles Zano's atomic units (pizano)

### Payment Processing Flow

1. **Invoice Creation**: Generates integrated addresses for payments
2. **Address Reservation**: Reserves addresses with proper labeling
3. **Fee Estimation**: Calculates dynamic fees from network
4. **Transaction Monitoring**: Continuously monitors for incoming payments
5. **Confirmation Tracking**: Tracks confirmations and updates invoice status

### Confirmation Policies

The plugin supports configurable confirmation thresholds:
- **High Speed**: 0 confirmations
- **Medium Speed**: 1 confirmation  
- **Low-Medium Speed**: 2 confirmations
- **Low Speed**: 6 confirmations

## Troubleshooting

### Common Issues

#### RPC Connection Failed
```bash
# Check Zano node status
curl -X POST http://localhost:32348/json_rpc \
  -H "Content-Type: application/json" \
  -d '{"jsonrpc":"2.0","id":"0","method":"getinfo"}'
```

#### Plugin Not Loading
- Verify .NET version compatibility (requires .NET 8.0)
- Check plugin file permissions
- Review BTCPay Server logs for errors

#### Payment Not Detected
- Verify RPC credentials
- Check network connectivity
- Ensure sufficient confirmations for speed policy

### Debug Mode
Enable debug logging in plugin configuration:

```json
{
  "Logging": {
    "LogLevel": {
      "BTCPayServer.Plugins.Zano": "Debug"
    }
  }
}
```

## Contributing

### Development Setup
1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

### Code Style
- Follow C# coding conventions
- Use async/await for I/O operations
- Implement proper error handling
- Add comprehensive logging

## License

MIT

## About

This plugin extends BTCPay Server to enable users to receive payments via Zano.

### Topics

plugin selfhosted crowdfunding zano point-of-sale zano-plugin btcpay btcpayserver zano-payment-gateway zano-payment-processor

### Resources

- [Zano Official Website](https://zano.org/)
- [Zano Documentation](https://docs.zano.org/)
- [BTCPay Server Documentation](https://docs.btcpayserver.org/)

---

**Warning**: This plugin shares a single Zano wallet across all the stores in the BTCPay Server instance. Use this plugin only if you are not sharing your instance.
