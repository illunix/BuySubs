terraform {
  required_version = ">= 0.12"
}

provider "aws" {
  region = "eu-west-2"
}

module "vpc" {
  source = "../vpc"
}

resource "aws_elasticache_cluster" "main" {
  cluster_id         = "cache-cluster"
  engine             = "memcached"
  node_type          = "cache.t3.small"
  num_cache_nodes    = 1
  port               = 11211
  security_group_ids = [module.vpc.aws_security_group_allow_all_id]
  apply_immediately  = true
  subnet_group_name  = aws_elasticache_subnet_group.main.id
}

resource "aws_elasticache_subnet_group" "main" {
  name       = "cache-subnet"
  subnet_ids = [module.vpc.aws_subnet_main_id, module.vpc.aws_subnet_second_id]
}
