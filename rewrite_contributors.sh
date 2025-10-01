#!/bin/bash
set -e

# Config
REPO_SSH="git@github.com:zano-list/btcpayserver-zano-integration.git"
USERNAME="zanolistdev"
EMAIL="reese@rlozy.com"

# Clean working dir
cd ~/Documents/Projects
rm -rf btcpayserver-zano-clean

# Fresh clone
git clone --mirror "$REPO_SSH" btcpayserver-zano-clean
cd btcpayserver-zano-clean

# Rewriting authors + committers
git filter-repo --force \
  --name-callback "return b'$USERNAME'" \
  --email-callback "return b'$EMAIL'"

# Push rewritten history back
git push --force --all origin
git push --force --tags origin

echo "✅ Done! Repo rewritten with contributor: $USERNAME <$EMAIL>"
