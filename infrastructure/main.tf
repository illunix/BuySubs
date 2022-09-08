terraform {
  required_version = ">= 0.12"
}

provider "aws" {
  region = "eu-west-2"
}

module "ec2" {
  source = "./resources/ec2"
}

module "rds" {
  source = "./resources/rds"
}

module "elasticcache" {
  source = "./resources/elasticache"
}
