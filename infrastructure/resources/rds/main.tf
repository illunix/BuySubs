terraform {
  required_version = ">= 0.12"
}

provider "aws" {
  region = "eu-west-2"
}

module "vpc" {
  source = "../vpc"  
}

resource "aws_rds_cluster" "default" {
  cluster_identifier      = "buy-subs-rds"
  engine                  = "aurora-postgresql"
  availability_zones      = ["eu-west-2a"]
  database_name           = var.db_name
  master_username         = var.username
  master_password         = var.password
}
