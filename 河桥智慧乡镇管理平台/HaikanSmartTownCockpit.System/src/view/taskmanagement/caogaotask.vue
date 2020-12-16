<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.vote.query.totalCount"
        :pageSize="stores.vote.query.pageSize"
        @on-page-change="handlePageChanged"
        @on-page-size-change="handlePageSizeChanged"
      >
        <div slot="searcher">
          <section class="dnc-toolbar-wrap">
            <Row :gutter="16">
              <Col span="16">
                <Form inline @submit.native.prevent>
                  <FormItem>
                    <Input
                      type="text"
                      search
					            style="width: 180px;"
                      :clearable="true"
                      v-model="stores.vote.query.kwbt"
                      placeholder="输入任务标题搜索..."
					            @on-search="handleSearch()"
                    >
                    </Input>
                  </FormItem>
                  <FormItem>
<Select v-model="stores.vote.query.kwfzr" :clearable="true" style="width: 180px;" placeholder="请选择负责人" filterable>
					  	<Option v-for="item in fuzerendata" :value="item.systemUserUUID" :key="item.systemUserUUID">{{item.realName}}</Option>
					  </Select>
                  </FormItem>
                  				  <FormItem>
              <Select v-model="stores.vote.query.zt" placeholder="状态" clearable>
                <Option value="0">进行中</Option>
				<Option value="1">审核中</Option>
				<Option value="2">已完成</Option>
              </Select>
            </FormItem>
				<FormItem>
              <Select v-model="stores.vote.query.yxj" placeholder="请选择优先级" clearable>
                <Option value="普通">普通</Option>
				<Option value="紧急">紧急</Option>
				<Option value="非常紧急">非常紧急</Option>
              </Select>
            </FormItem>
				  <FormItem>
				  <Button
				  v-can="'search'"
				    icon="md-search"
				    type="primary"
				    @click="handleSearch()"
				    title="查询">查询
				  </Button>
				  </FormItem>
				  </FormItem>
                </Form>
              </Col>
              <Col span="8" class="dnc-toolbar-btns">
                <ButtonGroup class="mr3">
                  <Button
                  v-can="'delete'"
                    class="txt-danger"
                    icon="md-trash"
                    title="删除"
                    @click="handleBatchCommand('delete')"
                  ></Button>
                  <Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
                </ButtonGroup>
                <Button
                  v-can="'create'"
                  icon="md-create"
                  type="primary"
                  @click="handleShowCreateWindow"
                  title="新增草稿"
                >新增草稿</Button>
              </Col>
            </Row>
          </section>
        </div>
        <Table
            slot="table"
            ref="tables"
            :border="false"
            size="small"
            :highlight-row="true"
            :data="stores.vote.data"
            :columns="stores.columns"
            @on-selection-change="handleSelectionChange"
            @on-refresh="handleRefresh"
            @on-page-change="handlePageChanged"
            @on-page-size-change="handlePageSizeChanged"
            @on-sort-change="handleSortChange"
          >
		  <template slot-scope="{row,index}" slot="auditStatus">
		    <span>{{getauditStatus(row.auditStatus)}}</span>
		  </template>
          <template slot-scope="{ row, index }" slot="action">
            <Poptip
              confirm
              :transfer="true"
              title="确定将此任务放入回收站?"
              @on-ok="handleDelete(row)"
              >
              <Tooltip placement="top" content="删除" :delay="1000" :transfer="true">
                <Button v-can="'delete'" type="error" size="small" shape="circle" icon="md-trash"></Button>
              </Tooltip>
            </Poptip>
            <Tooltip placement="top" content="编辑" :delay="1000" :transfer="true">
              <Button v-can="'edit'" type="primary" size="small" shape="circle" icon="md-create" @click="handleEdit(row)"></Button>
            </Tooltip>
        </template>
        </Table>
      </dz-table>
    </Card>
	
	
	<!-- 添加、修改的表单 -->
    <Drawer
      :title="formTitle"
      v-model="formModel.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
      >
      <Form :model="formModel.fields" ref="formtask" :rules="formModel.rules" label-position="top">
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="所属重点工作" prop="priorityUUID">
		<Select v-model="formModel.fields.priorityUUID" placeholder="所属重点工作" filterable>
			<Option v-for="item in prioritydata" :value="item.priorityUUID":title="item.priorityHeadlinelong">{{item.priorityHeadline}}</Option>
		</Select>
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="任务标题" prop="missionHeadline">
              <Input v-model="formModel.fields.missionHeadline" placeholder="请输入任务标题"/>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="负责人" prop="selectfuzename">
              <Input v-model="formModel.fields.selectfuzename" @on-change="selectfuze" style="width: 150px;" placeholder="请选择负责人" :readonly="true"/>
					<i-button type="info" @click="selectfuze(rowid)">选择负责人</i-button>
		<!-- <Select v-model="formModel.fields.principal" placeholder="负责人" filterable>
			<Option v-for="item in fuzerendata" :value="item.systemUserUUID">{{item.realName}}</Option>
		</Select> -->
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="优先级" prop="priority">
              <Select v-model="formModel.fields.priority">
                <Option value="普通">普通</Option>
				<Option value="紧急">紧急</Option>
				<Option value="非常紧急">非常紧急</Option>
              </Select>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="开始时间" prop="startTime">
             <Date-picker type="datetime" format="yyyy-MM-dd HH:mm" :options="options3" placeholder="请选择时间" @on-change="formModel.fields.startTime=$event" :value="formModel.fields.startTime" ></Date-picker>
            </FormItem>
          </Col>
		  <Col span="12">
		    <FormItem label="结束时间" prop="finishTime">
		     <Date-picker type="datetime" format="yyyy-MM-dd HH:mm" :options="options3" placeholder="请选择时间" @on-change="formModel.fields.finishTime=$event" :value="formModel.fields.finishTime"></Date-picker>
		    </FormItem>
		  </Col>
        </Row>
        <Row :gutter="16">
          <Col span="50">
            <FormItem label="任务描述" prop="missionDescribe">
              <Input
                v-model="formModel.fields.missionDescribe"
                placeholder="请输入任务描述"
                type="textarea"
                :rows="5"
              />
            </FormItem>
          </Col>
        </Row> 
        <Row :gutter="16">
          <!-- <Col span="12">
            <FormItem label="计划工时" prop="manhour">
              <Input-number :max="999999" :min="1" v-model="formModel.fields.manhour"></Input-number> 天
            </FormItem>
          </Col>-->
			    <Col span="12">
            <FormItem label="参与人" prop="selectcanyurenname">
              <Input v-model="formModel.fields.selectcanyurenname" @on-change="selectcanyu" style="width: 150px;" placeholder="请选择参与人" :readonly="true"/>
              <!-- <Button icon="md-checkmark-circle" type="ghost" @click="selectcanyu">选择参与人</Button> -->
              <i-button type="info" @click="selectcanyu(rowid)">选择参与人</i-button>
            </FormItem>
				  </Col>
          <Col span="12">
            <FormItem label="审批人" prop="selectshenpiname">
              <Input v-model="formModel.fields.selectshenpiname" @on-change="selectshenpi" style="width: 150px;" placeholder="请选择审批人" :readonly="true"/>
					<i-button type="info" @click="selectshenpi(rowid)">选择审批人</i-button>
              <!-- <Select v-model="formModel.fields.approver" placeholder="审批人" filterable clearable>
                <Option v-for="item in approver" :value="item.systemUserUUID">{{item.realName}}</Option>
              </Select> -->
            </FormItem>
          </Col>
        </Row>

        <Row :gutter="16">
		      <Col span="12">
            <FormItem label="创建人" prop="establishName">
              <Input v-model="formModel.fields.establishName" :readonly="true"  placeholder="创建人"/>
            </FormItem>
		      </Col>
        </Row>
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="handleSubmit">保存草稿</Button>
        <Button style="margin-left: 8px" type="warning" icon="md-close" v-if="caogaoshow" @click="chuangjian">创 建</Button>
      </div>
    </Drawer>
	


	    <Drawer
      title="选择审批人"
      v-model="formModelshenpi.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Form :model="formModelshenpi.fields" ref="formselect" label-position="top">


            <FormItem label="所属科室" prop="uuid">
						<Select v-model="formMode4.fields.uuid" @on-change="binddata(formMode4.fields.uuid)" placeholder="所属科室" filterable>
							<Option v-for="item in keshidatalist" :value="item.priorityUUID" :title="item.priorityHeadline">{{item.priorityHeadline}}</Option>
						</Select>
					</FormItem>
            <FormItem label="审批人" prop="fuze">
            <Transfer
                :data="formModelshenpi.fields.shenpileft"
                :target-keys="formModelshenpi.fields.shenpiright"
                :list-style="{width: '240px',height: '400px'}"
                :titles="['所有人员','选择的人员']"
                filterable
                @on-change="handleSchoolChange3"
              ></Transfer>
            </FormItem>

      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="formModelshenpi.opened = false">确 定</Button>
      </div>
    </Drawer>


    <Drawer
      title="选择负责人"
      v-model="formModelfuze.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Form :model="formModelfuze.fields" ref="formselect" label-position="top">


            <FormItem label="所属科室" prop="uuid">
						<Select v-model="formMode4.fields.uuid" @on-change="binddata(formMode4.fields.uuid)" placeholder="所属科室" filterable>
							<Option v-for="item in keshidatalist" :value="item.priorityUUID" :title="item.priorityHeadline">{{item.priorityHeadline}}</Option>
						</Select>
					</FormItem>
            <FormItem label="参与人" prop="fuze">
            <Transfer
                :data="formModelfuze.fields.fuzeleft"
                :target-keys="formModelfuze.fields.fuzeright"
                :list-style="{width: '240px',height: '400px'}"
                :titles="['所有人员','选择的人员']"
                filterable
                @on-change="handleSchoolChange2"
              ></Transfer>
            </FormItem>

      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="formModelfuze.opened = false">确 定</Button>
      </div>
    </Drawer>



    <Drawer
      title="选择参与人"
      v-model="formModelcanyu.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Form :model="formModelcanyu.fields" ref="formselect" label-position="top">

					<FormItem label="所属科室" prop="uuid">
						<Select v-model="formMode4.fields.uuid" @on-change="binddataid(formMode4.fields.uuid)" placeholder="所属科室" filterable>
							<Option v-for="item in keshidatalist" :value="item.priorityUUID" :title="item.priorityHeadline">{{item.priorityHeadline}}</Option>
						</Select>
					</FormItem>

            <FormItem label="参与人" prop="participant">
            <Transfer
                :data="formModelcanyu.fields.participantleft"
                :target-keys="formModelcanyu.fields.participantright"
                :list-style="{width: '240px',height: '400px'}"
                :titles="['所有人员','选择的人员']"
                filterable
                @on-change="handleSchoolChange"
              ></Transfer>
            </FormItem>

      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="formModelcanyu.opened = false">确 定</Button>
      </div>
    </Drawer>
	

  </div>
</template>

<script>
import DzTable from "_c/tables/dz-table.vue";
import config from '@/config';
import {
  getdatalist,
  createcaogao,
  baocuneditcaogao,
} from "@/api/taskpai/caogaotask";
import {
  getpersonaldiary,
  getfuzeren,
  getcanyuren,
  loaddata,
  baocunedit,
  binddataok2,
  getcanyuren2,
  binddataok3,
  binddataok,
  create,
  deletelist,
    keshidata,
systemuserlistuuid,
systemuserlistid
} from "@/api/taskpai/taskapis";

export default {
  name: "rbac_user_page",
  components: {
    DzTable
  },
  data() {
    return {
      commands: {
        delete: { name: "delete", title: "删除" },
      },
	  options3: {
	                  disabledDate (date) {
	                      return date && date.valueOf() < Date.now() - 86400000;
	                  }
	              },
		panduanrowid:0,
	  caogaoshow:false,
	  prioritydata:[],//重点工作
	  fuzerendata:[],//负责人
	  approver:[],//审批人
	  rowid:"",//选择行的id
      formModel: {
        opened: false,
        title: "创建任务",
        mode: "create",
        selection: [],
        fields: {
		  missionUUID:"",
          priorityUUID: "",//所属重点工作
          missionHeadline: "",//任务标题
          principal: "",//负责人
          selectfuzename:"",//选择的负责人姓名
          startTime: "",//开始时间
          finishTime: "",//结束时间
          missionDescribe:'',//任务描述
          priority:'',//优先级
          manhour:1,//计划工时
      approver:"",//审批人
      selectshenpiname:"",//审批人
		  participant:"",//选择的参与人id	
		  selectcanyurenname:"",//选择的参与人姓名
		  establishName:"",//创建人uuid
		  establisthtruename:"",//创建人姓名
		  establishTime:"",//创建时间
		  auditStatus:"",//审核状态
        },
        rules: {
          priorityUUID: [
            { type: "string", required: true, message: "请选择所属重点工作" }
          ],
          missionHeadline: [{ type: "string", required: true, message: "请输入任务标题" }],
          principal: [{ type: "string", required: true, message: "请选择负责人" }],
		  priority:[{type: "string", required: true, message: "请选择优先级" }],
		  startTime:[{type: "string", required: true, message: "请选择开始时间" }],
		  finishTime:[{type: "string", required: true, message: "请选择结束时间" }],
		  selectcanyurenname:[{type: "string", required: true, message: "请选择参与人" }],
		  //approver:[{type: "string", required: true, message: "请选择审批人" }]
        }
      },
      formModel2: {
        opened: false,
        title: "查看任务",
        mode: "create",
        selection: [],
        fields: {
		  missionUUID:"",
          priorityUUID: "",//所属重点工作
          missionHeadline: "",//任务标题
          principal: "",//负责人
          selectfuzename:"",//负责人
          startTime: "",//开始时间
          finishTime: "",//结束时间
          missionDescribe:'',//任务描述
          priority:'',//优先级
          manhourt:'',//计划工时
      approver:"",//审批人
      selectshenpiname:"",//审批人
		  participant:"",//选择的参与人id
		  selectcanyurenname:"",//选择的参与人姓名
		  establishName:"",//创建人uuid
		  establisthtruename:"",//创建人姓名
		  establishTime:"",//创建时间
        }
      },
      formModel3: {
        opened: false,
        title: "完成任务",
        mode: "create",
        selection: [],
        fields: {
		  missionUUID:"",
          priorityUUID: "",//所属重点工作
          missionHeadline: "",//任务标题
          principal: "",//负责人
          startTime: "",//开始时间
          finishTime: "",//结束时间
          missionDescribe:'',//任务描述
          priority:'',//优先级
          manhourt:'',//计划工时
      approver:"",//审批人
      selectshenpiname:"",//审批人
		  participant:"",//选择的参与人id
		  selectcanyurenname:"",//选择的参与人姓名
		  establishName:"",//创建人uuid
		  establisthtruename:"",//创建人姓名
		  establishTime:"",//创建时间
		  remark:"",//备注
        }
      },
	formModelcanyu:{
        opened: false,
        title: "选择人员",
        mode: "select",
		fields:{
			participantleft:[],
			participantright:[],
		}
  },
  		formModelfuze:{
        opened: false,
        title: "选择人员",
        mode: "select",
		fields:{
			fuzeleft:[],
			fuzeright:[],
		}
	},
	  			formModelshenpi:{
        opened: false,
        title: "选择人员",
        mode: "select",
		fields:{
			shenpileft:[],
			shenpiright:[],
		}
  },
  
  					formMode4: {
					fields: {
						id:"",
						uuid:"",
						name:""
					}
				},
				keshidatalist:[],
      stores: {
		  vote:{
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
			userguid:"",
            kwbt: "",
      kwfzr:"",
      zt:"",
      yxj:"",
			kwendtime:"",
            isDeleted: 0,
            status: -1,
            sort: [
              {
                direct: "DESC",
                field: "ID"
              }
            ]
          }, 
		  data: []
		  },
          columns: [
            { type: "selection", width: 50, key: "handle" },
            { title: "任务标题", key: "missionHeadline",ellipsis: true},
            { title: "所属重点工作", key: "priorityHeadline",ellipsis: true},
            { title: "负责人", key: "principal"},
            { title: "优先级", key: "priority"},

            { title: "开始时间",  ellipsis: true, tooltip: true, key: "startTime" },
            { title: "结束时间", key: "finishTime" },
            { title: "操作", align: "center", width: 150, className: "table-command-column",slot:"action" }
          ]
      },
      styles: {
        height: "calc(100% - 55px)",
        overflow: "auto",
        paddingBottom: "53px",
        position: "static"
      }
    };
  },
  computed: {
    formTitle() {
      if (this.formModel.mode === "create") {
        return "创建任务";
      }
      if (this.formModel.mode === "edit") {
        return "编辑任务";
      }
      return "";
    },
	selectedRowsId() {
	  return this.formModel.selection.map(x => x.id);
	}
  },
  methods: {
      //获取调度数据
      loadVoteinitiateList() {
		this.stores.vote.query.userguid=this.$store.state.user.userGuid;//当前登录账号guid
		//获取当前账号负责的任务信息
        getdatalist(this.stores.vote.query).then(res => {
          this.stores.vote.data = res.data.data;
          this.stores.vote.query.totalCount = res.data.totalCount;
        });
	  },
	  //***************************************************************************
      handleSortChange(column) {
        this.stores.query.sort.direction = column.order;
        this.stores.query.sort.field = column.key;
        this.loadVoteinitiateList();
      },
      //刷新
      handleRefresh() {
        this.loadVoteinitiateList();
      },
	  //获取选择的行数据
      handleSelectionChange(selection) {
        this.formModel.selection = selection;
      },
	  //翻页
	  handlePageChanged(page) {
	    this.stores.vote.query.currentPage = page;
	    this.loadVoteinitiateList();
	  },
      //显示条数改变
      handlePageSizeChanged(pageSize) {
        this.stores.vote.query.pageSize = pageSize;
        this.loadVoteinitiateList();
      },
	  //搜索
	  handleSearch(){
		  this.loadVoteinitiateList();
	  },
	  //点击添加打开窗口
	  handleShowCreateWindow(){
		  this.formModel.mode="create";//添加
      this.formModel.opened=true;//打开窗口
      this.$refs["formtask"].resetFields();//清空填写的表单信息
		 this.formModelfuze.fields.fuzeright=[]//重新打开窗口，清空选择的人员信息 负责人
		 this.formModelshenpi.fields.shenpiright=[]//重新打开窗口，清空选择的人员信息审批人
		 this.formModelcanyu.fields.participantright=[]//重新打开窗口，清空选择的人员信息 参与人
		  this.formModel.fields.establishName=this.$store.state.user.userName;//创建人
	  },
	  //点击编辑打开窗口
	  handleEdit(row){
		 this.formModel.mode="edit";//修改
		 this.formModel.opened=true;
		 this.caogaoshow=true;
     this.rowid=row.id;//保存选择行的id
		 this.formModelfuze.fields.fuzeright=[]//重新打开窗口，清空选择的人员信息 负责人
		 this.formModelshenpi.fields.shenpiright=[]//重新打开窗口，清空选择的人员信息审批人
		 this.formModelcanyu.fields.participantright=[]//重新打开窗口，清空选择的人员信息 参与人
		 loaddata({ id: row.id}).then(res=>{
			 this.formModel.fields=res.data.data
		 });
	  },
	  //重点工作下拉框
	  loadpersonaldiary(){
		  getpersonaldiary().then(res => {
			this.prioritydata = res.data.data
/* 			console.log("重点工作数据")
			console.log(this.prioritydata) */
		});
	  },
	  //负责人和审批人下拉框
	  loadfuzeren(){
		  getfuzeren().then(res => {
			this.approver=this.fuzerendata = res.data.data
		});
    },
    	  //穿梭框负责人 审批人
	  loaselect2(){
		  getcanyuren2().then(res=>{
		  	this.formModelshenpi.fields.shenpileft=this.formModelfuze.fields.fuzeleft=res.data.data
		  });
	  },
	  //穿梭框参与人
	  loaselect(){
		  getcanyuren().then(res=>{
		  			 this.formModelcanyu.fields.participantleft=res.data.data
		  });
    },
			//选择科室重新绑定人员信息
			binddata(data){

				systemuserlistuuid({uuid:data}).then(res => {
					this.formModelshenpi.fields.shenpileft=this.formModelfuze.fields.fuzeleft=res.data.data
				})
				
			},
			binddataid(data){
				systemuserlistid({uuid:data}).then(res => {
					console.log(res)
					this.formModelcanyu.fields.participantleft=res.data.data
				})
			},
			keshijiazai(){
				keshidata().then(res => {
					this.keshidatalist=this.keshidatalistid=res.data.data;
					
				})
			},



//打开选择审批人窗口
selectshenpi(rowid){
		  this.formModelshenpi.opened=true
		  //修改
		  if(rowid!="")
		  {
		binddataok3({ id: rowid}).then(res => {
			if(this.panduanrowid!=rowid)//判断是否是本行的数据
			{
				//不是本行数据
			this.formModelshenpi.fields.shenpiright=[]//清除上次选择的人员	
			}
			//如果没有选择人员则显示数据库中的内容（表示选择了新行）
			if(this.formModelshenpi.fields.shenpiright.length==0)
			{

					if(res.data.data.length>0)
					{
					    for (let i = 0; i < res.data.data.length; i++) {
							//显示数据库保存的人员
					        this.formModelshenpi.fields.shenpiright = this.formModelshenpi.fields.shenpiright.concat(res.data.data[i].key);
					    }
					}			
			}
					this.panduanrowid=rowid;//将本行的id保存
				});
		  }
},
	  //选择审批人
	  handleSchoolChange3(newTargetKeys, direction, moveKeys){
		  this.formModelshenpi.fields.shenpiright = newTargetKeys;
		  this.formModel.fields.selectshenpiname=this.formModel.fields.approver="";//每次选择则负责人都重新赋值
	    for(let i=0;i<this.formModelshenpi.fields.shenpiright.length;i++)
	    {
	        for(let j=0;j<this.formModelshenpi.fields.shenpileft.length;j++)
	        {
	            if(this.formModelshenpi.fields.shenpiright[i]===this.formModelshenpi.fields.shenpileft[j].key)
	            {
	                this.formModel.fields.selectshenpiname  += this.formModelshenpi.fields.shenpileft[j].label+",";
	                this.formModel.fields.approver += this.formModelshenpi.fields.shenpileft[j].key+",";
	            }
	        }
	    }

	  },
    

    //打开选择负责人窗口
selectfuze(rowid){

		  this.formModelfuze.opened=true
		  //修改
		  if(rowid!="")
		  {
			  console.log(rowid)
		binddataok2({ id: rowid}).then(res => {
			if(this.panduanrowid!=rowid)//判断是否是本行的数据
			{
				//不是本行数据
			this.formModelfuze.fields.fuzeright=[]//清除上次选择的人员	
			}
			//如果没有选择人员则显示数据库中的内容（表示选择了新行）
			if(this.formModelfuze.fields.fuzeright.length==0)
			{

					if(res.data.data.length>0)
					{
					    for (let i = 0; i < res.data.data.length; i++) {
							//显示数据库保存的人员
					        this.formModelfuze.fields.fuzeright = this.formModelfuze.fields.fuzeright.concat(res.data.data[i].key);
					    }
					}			
			}
					this.panduanrowid=rowid;//将本行的id保存
				});
		  }
},
	  //选择负责人
	  handleSchoolChange2(newTargetKeys, direction, moveKeys){
		  this.formModelfuze.fields.fuzeright = newTargetKeys;
		  this.formModel.fields.selectfuzename=this.formModel.fields.principal="";//每次选择则负责人都重新赋值
	    for(let i=0;i<this.formModelfuze.fields.fuzeright.length;i++)
	    {
	        for(let j=0;j<this.formModelfuze.fields.fuzeleft.length;j++)
	        {
	            if(this.formModelfuze.fields.fuzeright[i]===this.formModelfuze.fields.fuzeleft[j].key)
	            {
	                this.formModel.fields.selectfuzename  += this.formModelfuze.fields.fuzeleft[j].label+",";
	                this.formModel.fields.principal += this.formModelfuze.fields.fuzeleft[j].key+",";
	            }
	        }
	    }

	  },



	  //打开选择参与人窗口
	  selectcanyu(rowid){
		  this.formModelcanyu.opened=true
		  //修改
		  if(rowid!="")
		  {
		binddataok({ id: rowid}).then(res => {
			
			if(this.panduanrowid!=rowid)//判断是否是本行的数据
			{
				//不是本行数据
			this.formModelcanyu.fields.participantright=[]//清除上次选择的人员	
			}
			//如果没有选择人员则显示数据库中的内容（表示选择了新行）
			if(this.formModelcanyu.fields.participantright.length==0)
			{

					if(res.data.data.length>0)
					{
					    for (let i = 0; i < res.data.data.length; i++) {
							//显示数据库保存的人员
					        this.formModelcanyu.fields.participantright = this.formModelcanyu.fields.participantright.concat(res.data.data[i].key);
					    }
					}			
			}
					this.panduanrowid=rowid;//将本行的id保存
				});
		  }

	  },
	  //选择参与人
	  handleSchoolChange(newTargetKeys, direction, moveKeys){
		  this.formModelcanyu.fields.participantright = newTargetKeys;
		  this.formModel.fields.selectcanyurenname=this.formModel.fields.participant="";//每次选择则参与人都重新赋值
	    for(let i=0;i<this.formModelcanyu.fields.participantright.length;i++)
	    {
	        for(let j=0;j<this.formModelcanyu.fields.participantleft.length;j++)
	        {
	            if(this.formModelcanyu.fields.participantright[i]===this.formModelcanyu.fields.participantleft[j].key)
	            {
	                this.formModel.fields.selectcanyurenname  += this.formModelcanyu.fields.participantleft[j].label+",";
	                this.formModel.fields.participant += this.formModelcanyu.fields.participantleft[j].key+",";
	            }
	        }
	    }
/* 		  console.log("选择的人员")
		  console.log(this.selectcanyurenname) */
	  },
	  //点击保存
	  handleSubmit(){
		  
		  let valid = this.validateForm();
		  if(valid)
		  {
		  if (this.formModel.mode === "create") {//保存添加的内容
		  this.formModel.fields.establisthtruename=this.$store.state.user.userGuid;//当前登录账号guid(数据库保存创建人)
			  createcaogao(this.formModel.fields).then(res => {
			    if (res.data.code === 200) {
			      this.$Message.success(res.data.message);
				  this.formModel.opened=false;
				  this.loadVoteinitiateList();
			    } else {
			      this.$Message.warning(res.data.message);
			    }
			  });
		  }
		  if (this.formModel.mode === "edit") {//保存修改的内容

			   baocunedit(this.formModel.fields).then(res => {
			    if (res.data.code === 200) {
			      this.$Message.success(res.data.message);
				  this.formModel.opened=false;
				  this.loadVoteinitiateList();
			    } else {
			      this.$Message.warning(res.data.message);
			    }
			  });
		  }				  
		  }

	  },
	  
	  //验证表单信息
	  			validateForm() {
	  			  let _valid = false;
	  			  this.$refs["formtask"].validate(valid => {
	  			    if (!valid) {
	  			      this.$Message.error("请完善表单信息");
	  			    } else {
	  			      _valid = true;
	  			    }
	  			  });
	  			  return _valid;
	  			},
	  //创建
	  chuangjian(){
		  this.$Modal.confirm({
		  	      title: "操作提示",
		  	      content:
		  	        "<p>确定要创建到任务吗?</p>",
		  	      loading: true,
		  	      onOk: () => {
                     //保存修改的内容并移除草稿箱
		  			  baocuneditcaogao(this.formModel.fields).then(res => {
		  			    if (res.data.code === 200) {
		  			      this.$Message.success(res.data.message);
		  				  this.formModel.opened=false;
		  				  this.loadVoteinitiateList();
						  this.$Modal.remove();//关闭提示框
		  			    } else {
                  this.$Message.warning(res.data.message);
                  this.$Modal.remove();//关闭提示框
		  			    }
		  			  });
		  	   
		  	      }
		  	    });

		  
	  },
	  
	  //删除多条数据
	    handleBatchCommand(command) {
	    if (!this.selectedRowsId || this.selectedRowsId.length <= 0) {
	      this.$Message.warning("请选择至少一条数据");
	      return;
	    }
	    this.$Modal.confirm({
	      title: "操作提示",
	      content:
	        "<p>确定要将所选任务放入回收站吗?</p>",
	      loading: true,
	      onOk: () => {
/* 			  console.log("删除选择的")
	  		console.log(this.selectedRowsId.join(",")) */
	       deletelist({ ids: this.selectedRowsId.join(",")}).then(res => {
	         if (res.data.code === 200) {
	           this.$Message.success(res.data.message);
	  			 this.loadVoteinitiateList();
	  			 this.$Modal.remove();//关闭提示框
	         } else {
	           this.$Message.warning(res.data.message);
	         }
	       });
	   
	      }
	    });
	  },
	  //删除单条数据
    handleDelete(row) {
      /* console.log(row.id) */
	  deletelist({ ids: row.id}).then(res => {
	    if (res.data.code === 200) {
	      this.$Message.success(res.data.message);
	  		this.loadVoteinitiateList();		  
		  $Message.success("操作成功");

	    } else {
	      this.$Message.warning(res.data.message);
	    }
	  });
    },
	  //查看
	  lookEwq(row){
		  this.formModel2.opened=true;
		 loaddata({ id: row.id}).then(res=>{
			 this.formModel2.fields=res.data.data
		 });
	  },
	  getauditStatus(auditStatus){
		  var auditStatusText = "未知";
		  switch (auditStatus) {
		    case "0":
		      auditStatusText = "进行中";
		      break;
		    case "1":
		      auditStatusText = "审核中";
		      break;
		    case "2":
		      auditStatusText = "已完成";
		      break;
		  }
		  return auditStatusText;
	  }
	 },
  mounted() {
	  this.loadVoteinitiateList();//加载所有数据
    this.loadpersonaldiary();//重点工作下拉框
    this.keshijiazai();//科室下拉框
	  this.loadfuzeren();//审批人下拉框
    this.loaselect();//穿梭框参与人
    this.loaselect2();//穿梭框负责人
  }
}
</script>

<style>
</style>


