#!/bin/sh

echo "postCreateCommand.sh"
echo "--------------------"

sudo apt-get update
# sudo apt-get install -y xdg-utils python3-pip

# echo "Setup pre-commit"
#pre-commit
#pre-commit install --install-hooks
#pre-commit run --all-files --verbose
#pre-commit autoupdate

sudo chmod +x .devcontainer/postStartCommand.sh
