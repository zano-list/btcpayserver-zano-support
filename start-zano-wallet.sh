#!/bin/sh
set -x  # prints every command before running
exec 2>&1  # send errors to stdout so they appear in logs
echo "Starting wallet creation..."
WALLET_DIR="/wallet"
WALLET_FILE="wallet-aud"
SEED_FILE="$WALLET_DIR/seed.txt"
PASSWORD_FILE="$WALLET_DIR/password.txt"
FULL_PATH="$WALLET_DIR/$WALLET_FILE"


# Check for seed file
if [ ! -f "$SEED_FILE" ]; then
  echo "❌ Seed file not found at $SEED_FILE"
  exit 1
fi

# Read seed and password
SEED=$(cat "$SEED_FILE")
PASSWORD=$(cat "$PASSWORD_FILE" 2>/dev/null || echo "zanowallet")

#rm -f "$FULL_PATH" 

if [ ! -f "$FULL_PATH" ]; then
  echo "Restoring wallet from seed..." 
  exec simplewallet --restore-wallet="$FULL_PATH" --daemon-address zano-daemon:11211 --password "$PASSWORD" <<EOF
$SEED
refresh
exit
EOF
else
 echo "Wallet already exists. Starting RPC mode..."
  exec simplewallet \
    --wallet-file "$FULL_PATH" \
    --password "$PASSWORD" \
    --rpc-bind-port=11233 \
    --rpc-bind-ip=0.0.0.0 \
    --daemon-address=zano-daemon:11211 \
    --log-level=1
fi