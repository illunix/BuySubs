terraform {
  required_version = ">= 0.12"
}

provider "aws" {
  region = "eu-west-2"
}

module "vpc" {
  source = "../vpc"  
}

resource "aws_rds_cluster" "main" {
  serverlessv2_scaling_configuration  {
    min_capacity = 2
    max_capacity = 4
  }

  engine                  = "aurora-postgresql"
  availability_zones      = ["eu-west-2a"]
  database_name           = "BuySubs"
  master_username         = "foo"
  master_password         = "baregeogoevne"
  vpc_security_group_ids = [module.vpc.aws_security_group_allow_all_id]
  db_subnet_group_name   = aws_db_subnet_group.main.id
}

resource "aws_rds_cluster_instance" "main" {
  count              = 1
  cluster_identifier = aws_rds_cluster.main.id
  instance_class     = "db.serverless"
  engine             = aws_rds_cluster.main.engine
  engine_version     = aws_rds_cluster.main.engine_version
}

resource "aws_db_subnet_group" "main" {
  subnet_ids  = [module.vpc.aws_subnet_main_id, module.vpc.aws_subnet_second_id]
}