terraform {
  required_version = ">= 0.12"
}

provider "aws" {
  region = "eu-west-2"
}

data "vpc_access" "exists" {
  team_id = "my-team-id"
  workspace_id = "my-workspace-id"
}

resource "aws_vpc" "default" {
  cidr_block = "10.0.0.0/16"
}

resource "aws_subnet" "subnet_1" {
  vpc_id            = aws_vpc.default.id
  cidr_block        = "10.0.1.0/24"
  availability_zone = "eu-west-2a"
}

resource "aws_subnet" "subnet_2" {
  vpc_id            = aws_vpc.default.id
  cidr_block        = "10.0.2.0/24"
  availability_zone = "eu-west-2b"
}

resource "aws_db_subnet_group" "default" {
  name        = "buy-subs-prod-sb-group"
  subnet_ids  = [aws_subnet.subnet_1.id, aws_subnet.subnet_2.id]
}

resource "aws_security_group" "default" {
  name          = "buy-subs-sg"
  vpc_id        = aws_vpc.default.id

  ingress {
    from_port   = 0
    to_port     = 65535
    protocol    = "TCP"
    cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

