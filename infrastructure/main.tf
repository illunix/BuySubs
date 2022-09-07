terraform {
  required_version = ">= 0.12"
}

provider "aws" {
  region = "eu-west-2"
}

module "rds" {
  source = "./resources/rds"
}
